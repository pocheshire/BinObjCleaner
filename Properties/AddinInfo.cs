using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly:Addin(
    "BinObjCleaner", 
    Namespace = "BinObjCleaner",
    Version = "1.0"
)]

[assembly:AddinName("BinObjCleaner")]
[assembly:AddinCategory("BinObjCleaner")]
[assembly:AddinDescription("BinObjCleaner")]
[assembly:AddinAuthor("Maxim Evtukh")]

[assembly:AddinDependency("::MonoDevelop.Core", MonoDevelop.BuildInfo.Version)]
[assembly:AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.Version)]
