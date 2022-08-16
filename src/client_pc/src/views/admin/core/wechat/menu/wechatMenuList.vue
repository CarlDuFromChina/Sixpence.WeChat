<template>
  <div :style="{ height: '100%', overflowY: 'auto' }">
    <sp-header>
      <div>
        <div style="display: inline-block">
          <slot name="buttons"></slot>
        </div>
        <!-- 按钮组 -->
        <div style="display: inline-block">
          <a-tooltip title="发布">
            <a-button :icon="'check'" @click="publish()" style="margin-right: 10px"></a-button>
          </a-tooltip>
          <a-tooltip title="创建">
            <a-button :icon="'plus'" @click="create()" style="margin-right: 10px"></a-button>
          </a-tooltip>
          <a-tooltip title="删除">
            <a-button :icon="'delete'" @click="remove()" style="margin-right: 10px"></a-button>
          </a-tooltip>
        </div>
      </div>
    </sp-header>
    <a-row :gutter="24">
      <a-col :span="6">
        <a-tree
          :style="{ 'padding': '0 12px'}"
          draggable
          show-line
          :replaceFields="{title: 'name'}"
          :tree-data="tableData"
          :auto-expand-parent="true"
          :default-expand-all="true"
          @drop="onDrop"
          @select="onSelect"
        />
      </a-col>
      <a-col :span="18">
        <editComponent ref="edit" :data="selectedRow" @save="save" :style="{ 'padding': '0 12px'}"></editComponent>
      </a-col>
    </a-row>
  </div>
</template>

<script>
import editComponent from './wechatMenuEdit.vue';

export default {
  name: 'sysMenuList',
  components: { editComponent },
  data() {
    return {
      controllerName: 'wechat_menu',
      selectedRow: {},
      tableData: [],
    };
  },
  beforeCreate() {
    sp.get('api/wechat_menu').then(resp => {
      var buttons = resp.selfmenu_info.button;
      var handleChildren = (buttons) => {
        buttons.forEach((e) => {
          if (e.sub_button) {
            e.children = e.sub_button.list;
            handleChildren(e.children);
          }
        });
      };
      handleChildren(buttons);
      this.tableData = buttons;
    });
  },
  methods: {
    onDrop(info) {
      console.log(info);
      const dropKey = info.node.title;
      const dragKey = info.dragNode.title;
      const dropPos = info.node.pos.split('-');
      const dropPosition = info.dropPosition - Number(dropPos[dropPos.length - 1]);
      const loop = (data, key, callback) => {
        data.forEach((item, index, arr) => {
          if (item.name === key) {
            return callback(item, index, arr);
          }
          if (item.children) {
            return loop(item.children, key, callback);
          }
        });
      };
      const data = [...this.tableData];

      // Find dragObject
      let dragObj;
      loop(data, dragKey, (item, index, arr) => {
        arr.splice(index, 1);
        dragObj = item;
      });
      if (!info.dropToGap) {
        // Drop on the content
        loop(data, dropKey, item => {
          item.children = item.children || [];
          // where to insert 示例添加到尾部，可以是随意位置
          item.children.push(dragObj);
        });
      } else if (
        (info.node.children || []).length > 0 && // Has children
        info.node.expanded && // Is expanded
        dropPosition === 1 // On the bottom gap
      ) {
        loop(data, dropKey, item => {
          item.children = item.children || [];
          // where to insert 示例添加到尾部，可以是随意位置
          item.children.unshift(dragObj);
        });
      } else {
        let ar;
        let i;
        loop(data, dropKey, (item, index, arr) => {
          ar = arr;
          i = index;
        });
        if (dropPosition === -1) {
          ar.splice(i, 0, dragObj);
        } else {
          ar.splice(i + 1, 0, dragObj);
        }
      }
      this.tableData = data;
    },
    onClose() {
      this.selectedRow = {};
    },
    onSelect(selectedKeys, info) {
      var title = info.node.title;
      this.selectedRow = this.findItem(title);
      this.$refs.edit.pageState = 'edit';
    },
    findItem(title) {
      var item = {};
      var loop = (data, name) => {
        data.forEach(e => {
          if (e.name === name) {
            item = e;
          }
          if (e.children && e.children.length > 0) {
            loop(e.children, name);
          }
        })
      }
      loop(this.tableData, title);
      return item;
    },
    save(type, data) {
      if (type === 'create') {
        this.tableData = [...this.tableData, data];
      }
    },
    create() {
      this.selectedRow = {};
      this.$refs.edit.pageState = 'create';
    },
    remove() {
      if (sp.isNullOrEmpty(this.selectionIds)) {
        this.$message.warning('请选择一项，再进行删除');
      } else {
        var removeItem = (name, data) => {
          var i = data.findIndex(e => e.name === name);
          if (i !== -1) {
            data.splice(i, 1);
          } else {
            data.forEach(e => {
              if (e.children && e.children.length > 0) {
                removeItem(name, e.children);
              }
            });
          }
        };
        this.selectionIds.forEach(name => removeItem(name, this.tableData));
      }
    },
    publish() {
      this.$confirm({
        title: '提示',
        content: '是否确认发布菜单?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.post('api/wechat_menu', { button: this.tableData }).then(() => {
            this.$message.success('发布成功');
          });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    }
  }
};
</script>
