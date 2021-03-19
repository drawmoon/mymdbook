# 快速搭建在线文档

- [初始化项目](#初始化项目)
- [定制侧边栏](#定制侧边栏)
- [部署在 Github Pages](#部署在 Github Pages)

## 初始化项目

> [docsify - 一个神奇的文档网站生成器。](https://github.com/docsifyjs/docsify)

安装 `docsify-cli`

```bash
npm install docsify-cli -g
```

初始化项目

```bash
docsify init ./docs
```

## 定制侧边栏

设置`loadSidebar`为`true`

```html
<!-- index.html -->

<script>
  window.$docsify = {
    name: 'dcoument',
    repo: '',

    loadSidebar: true
  }
</script>
```

新建`_sidebar.md`文件

```markdown
- [首页](/)
- [介绍](intro.md)
  - [快速入门](quickstart.md)
```

## 部署在 Github Pages

Settings > Options > GitHub Pages

在 Source 一栏，选择分支，比如`master`，再选择 `/docs` 文件夹，最后点击保存
