   M S B A M L   `   `   `  ????F  BJHoney_Flow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:JHoney_Flow.View.RepoLv2JHoney_Flow.View.RepoLv2  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      =#clr-namespace:JHoney_Flow.ConverterJHoney_Flow.Converter  5
      C&clr-namespace:JHoney_Flow.View.RepoLv2JHoney_Flow.View.RepoLv2  5      e7clr-namespace:JHoney_Flow.View.RepoLv2.DataListRootPath)JHoney_Flow.View.RepoLv2.DataListRootPath  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      '    !JHoney_Flow.View.RepoLv2.TestView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:JHoney_Flow.View.RepoLv2     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      S OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59'cmdhttp://www.codeplex.com/prism  5	      -cvt#clr-namespace:JHoney_Flow.Converter  5
      1View&clr-namespace:JHoney_Flow.View.RepoLv2  5      IPathSetView7clr-namespace:JHoney_Flow.View.RepoLv2.DataListRootPath  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      .??5      ?   ? ColumnDefinitions  5   
   ?? 5      $??* ?6   ?? 5      $
??auto ?6   .+?5   
   ? $??0??6    ? RowDefinitions 5      ?? 5      $
??auto ?6    ?? 5      $??* ?6    ?? 5      $
??auto ?6    ?? 5      $??* ?6    ?? 5      $
??auto ?6    .+?5       D   >JHoney_Flow.View.RepoLv2.DataListRootPath.DataListRootPathView   ? DataContext 6/   ?? *DataListRootPathViewModel+#  LiveCharts.Wpf.CartesianChart  5!      -   5          Name$ lvcChart??5!   !   $??1??63   .  (System.Windows.Interactivity.Interaction   Triggers 5"      /  )System.Windows.Interactivity.EventTrigger  5#         	EventName$ Loaded??6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5$      -  'Prism.Interactivity.InvokeCommandAction     Command 62   ?? *LoadedEvent+   CommandParameter 6R   ?? 	 ?? ElementName$	 lvcChart??5%      5'      ? 5)      $??2??6   $??Stretch=?6'   $??15??6?   
 ? Value
 6L   ?? *ProgressValue+ ? Maximum 6l   ?? *ProgressMax+6?   ?? 5*      $??2??6   $??Stretch=?6%   $??Stretch=?6C   $
??Bold"?6_    ?? TextAlignment$ Center=?6q   ??6?   ?? *ProgressPercent+ ?? StringFormat$ {0:F2} %???? 5+      $
??Auto=?6   $??3??6@   .??5,      ?? ??6   ?? *LogAll+??5-      ?? 5.      .R?5/      ?? ??6(   ??  ?? Mode$ OneWay=?50      52      53      ? 54      $??4??6    55      ?? 56      $??* ?6$   ?? 57      $
??auto ?6$   .+?5:      9? $??Test Settings??6    9? ExpandDirection$
 Down=?62   $??0??6I   .??5;      ??  ?? Orientation$ Vertical=?6!   .+?5<      ?   5=      ?? 5>   "   $
??auto ?63   ?? 5?   "   $
??auto ?63    5A      ?? 5B   "   $
??auto ?60   ?? 5C   "   $
??auto ?60   ?? 5D   "   $
??auto ?60   ?? 5E   "   $
??auto ?60   ?? 5F   "   $
??auto ?60   .+?5I      ?? $??0??6(   $??0??68   $??Threshold : ??6E   $??Center=?6Y   *  $MahApps.Metro.Controls.NumericUpDown  5J      $??1??65   $??0??6E   $	??100??6R      Interval$ 1Q?6v      Minimum$ 1Q?6?      Value 6^   ?? *Epoch+{?6?   ?? *
IsTraining+ ?? 	Converter 2  ,JHoney_Flow.Converter.BooleanInvertConverter  ?? 5K      $??0??6)   $??2??69   $??1??6M   .+?5L   "   ?? $??Train??6+    d? 	IsChecked 6;   ?? *UsingTrainSet+?? 5M   "   $??
Validation??6+    6@   ?? *UsingValidationSet+?? 5N   "   $
??Test??6+    6:   ?? *UsingTestSet+5O      5P      5Q      5R      ? 5S      $??1??6     5T      ?? 5U      $??* ?6+   ?? 5V      $??* ?6+   .+?5X      ?? $??0??6   $
??Test??6-   $??10q?6<   ??6H   ?? *CommandTest+{?6h   ?? *
IsTraining+   ?? 5Y      $??1??6   $
??Stop??6-   $??10q?6<   ??6H   ?? *CommandStop+{?6h   ?? *
IsTraining+5Z      5[      5^      ? 5a   
   $??1??6   .+?5b      ,	   &JHoney_Flow.View.RepoLv2.ModelListView	   6!   ?? *ModelListViewModel+5c      5d      5e      