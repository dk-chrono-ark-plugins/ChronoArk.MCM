游戏内模组配置菜单和自定义UI渲染工具

> source: https://github.com/dk-chrono-ark-plugins/ChronoArk.MCM


### 您是模组用户：

MCM引入了一个直观的游戏内配置菜单，允许在对局中就能修改其他模组的设置。


### 您是模组开发者：

> API 版本: V1

这个工具包含两个主要特性：

**自动配置迁移**：
如果您不需要自定义UI元素，无需进行任何更改。您的模组的设置将通过模组配置菜单（MCM）自动迁移，且不会造成破坏性影响, 仍然可以通过游戏内模组界面编辑设置。

**自定义UI元素和页面**：
对于那些希望引入自定义UI元素或页面的开发者，您可以在项目中将MCM设为依赖，只需引用MCM并使用其API来根据需求定制设置菜单。~~详尽的~~API文档在游戏内和源代码中均可查阅。


注意：随着我使用MCM API适配个人的其他模组，将会按需发布更多功能。
