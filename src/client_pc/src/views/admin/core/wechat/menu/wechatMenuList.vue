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
        </div>
      </div>
    </sp-header>
    <div :style="{margin: '10px'}">
      <a-row :gutter="24">
        <a-col :span="6">
          <a-row>
            <a-tooltip title="创建新菜单">
              <a-button :icon="'plus'" @click="create()" size="small"></a-button>
            </a-tooltip>
          </a-row>
          <a-row>
            <a-tree
              draggable
              show-line
              :expanded-keys="expandedKeys"
              :replaceFields="{ title: 'name' }"
              :tree-data="tableData"
              :auto-expand-parent="true"
              :default-expand-all="true"
              @drop="onDrop"
            >
              <template #title="{ key: treeKey, name }">
                <a-dropdown :trigger="['contextmenu']">
                  <span>{{ name }}</span>
                  <template #overlay>
                    <a-menu @click="({ key: menuKey }) => onContextMenuClick(treeKey, menuKey)">
                      <a-menu-item key="add">新增子菜单</a-menu-item>
                      <a-menu-item key="edit">编辑</a-menu-item>
                      <a-menu-item key="delete">删除</a-menu-item>
                    </a-menu>
                  </template>
                </a-dropdown>
              </template>
            </a-tree>
          </a-row>
        </a-col>
        <a-col :span="18">
          <editComponent ref="edit" :data="selectedRow" @save="save" @clear="clear"></editComponent>
        </a-col>
      </a-row>
    </div>
  </div>
</template>

<script>
import editComponent from './wechatMenuEdit.vue';
import { uuid } from '@sixpence/web-core'

export default {
  name: 'sysMenuList',
  components: { editComponent },
  data() {
    return {
      controllerName: 'wechat_menu',
      expandedKeys: [],
      selectedRow: {},
      tableData: []
    };
  },
  beforeCreate() {
    sp.get('api/wechat_menu').then(resp => {
      var buttons = resp.selfmenu_info.button;
      var handleChildren = buttons => {
        buttons.forEach(e => {
          if (e.sub_button) {
            e.children = e.sub_button.list;
            handleChildren(e.children);
          }
          if (sp.isNullOrEmpty(e.key)) {
            e.key = uuid.generate()
          }
        });
      };
      handleChildren(buttons);
      buttons.forEach(e => {
        if (!sp.isNullOrEmpty(e.children)) {
          this.expandedKeys.push(e.key);
        }
      })
      this.tableData = buttons;
    });
  },
  methods: {
    onDrop(info) {
      const dropKey = info.node.eventKey;
      const dragKey = info.dragNode.eventKey;
      const dropPos = info.node.pos.split('-');
      const dropPosition = info.dropPosition - Number(dropPos[dropPos.length - 1]);
      const loop = (data, key, callback) => {
        data.forEach((item, index, arr) => {
          if (item.key === key) {
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
    clear() {
      this.selectedRow = {};
    },
    onContextMenuClick(treeKey, menuKey) {
      switch (menuKey) {
        case 'delete': {
          var loop = (key, data) => {
            var i = data.findIndex(e => e.key === key);
            if (i !== -1) {
              data.splice(i, 1);
              this.$forceUpdate();
            } else {
              data.forEach(e => {
                if (e.children && e.children.length > 0) {
                  loop(key, e.children);
                }
              });
            }
          };
          loop(treeKey, this.tableData);
          break;
        }
        case 'edit': {
          this.selectedRow = this.findItem(treeKey);
          this.$refs.edit.pageState = 'edit';
          break;
        }
        case 'add': {
          this.selectedRow = { name: '子菜单', type: 'text', key: uuid.generate() };
          this.$refs.edit.pageState = 'create';
          var parent = this.findItem(treeKey);
          if (parent.children) {
            parent.children.push(this.selectedRow);
          } else {
            parent.children = [this.selectedRow];
          }
          break;
        }
        default:
          break;
      }
    },
    findItem(key) {
      var item = {};
      var loop = (data, key) => {
        data.forEach(e => {
          if (e.key === key) {
            item = e;
          }
          if (e.children && e.children.length > 0) {
            loop(e.children, key);
          }
        });
      };
      loop(this.tableData, key);
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
    publish() {
      this.$confirm({
        title: '提示',
        content: '是否确认发布菜单?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          var loop = data => {
            data.forEach(item => {
              if (item.children) {
                if (!item.sub_button) {
                  item.sub_button = { list: item.children };
                }
                loop(item.sub_button.list);
              }
            });
          };
          loop(this.tableData);
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
