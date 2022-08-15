publish.server:
	docker buildx build --platform=linux/amd64 -t carldu/wechat-server:latest -f "./src/server/Sixpence.WeChat/Dockerfile" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Sixpence.WeChat" "./src/server" 
	docker push carldu/wechat-server:latest

publish.pc:
	docker buildx build --platform=linux/amd64 -t carldu/wechat-html-pc:latest -f "./src/client_pc/Dockerfile" "./src/client_pc"
	docker push carldu/wechat-html-pc:latest

publish:
	make publish.server
	make publish.pc