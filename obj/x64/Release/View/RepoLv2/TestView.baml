   M S B A M L   `   `   `  ???? F  BJHoney_Flow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:JHoney_Flow.View.RepoLv2JHoney_Flow.View.RepoLv2  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity =#clr-namespace:JHoney_Flow.ConverterJHoney_Flow.Converter  C&clr-namespace:JHoney_Flow.View.RepoLv2JHoney_Flow.View.RepoLv2  e7clr-namespace:JHoney_Flow.View.RepoLv2.DataListRootPath)JHoney_Flow.View.RepoLv2.DataListRootPath  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf '    !JHoney_Flow.View.RepoLv2.TestView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:JHoney_Flow.View.RepoLv2     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  S OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59'cmdhttp://www.codeplex.com/prism  -cvt#clr-namespace:JHoney_Flow.Converter  1View&clr-namespace:JHoney_Flow.View.RepoLv2  IPathSetView7clr-namespace:JHoney_Flow.View.RepoLv2.DataListRootPath  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  .???   ? ColumnDefinitions  ?? $??* ??? $
??auto ?.+?? $??0?? ? RowDefinitions ?? $
??auto ??? $??* ??? $
??auto ??? $??* ??? $
??auto ?.+?D   >JHoney_Flow.View.RepoLv2.DataListRootPath.DataListRootPathView   ? DataContext ?? *DataListRootPathViewModel+#  LiveCharts.Wpf.CartesianChart  -     Name$ lvcChart??$??1??.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded??.  (System.Windows.Interactivity.TriggerBase   Actions. -  'Prism.Interactivity.InvokeCommandAction     Command ?? *LoadedEvent+   CommandParameter ?? 	 ?? ElementName$	 lvcChart??? $??2??$??Stretch=?$??15??
 ? Value
 ?? *ProgressValue+ ? Maximum ?? *ProgressMax+?? $??2??$??Stretch=?$??Stretch=?$
??Bold"? ?? TextAlignment$ Center=????? *ProgressPercent+ ?? StringFormat$ {0:F2} %???? $
??Auto=?$??3??.???? ???? *LogAll+???? .R??? ????  ?? Mode$ OneWay=?? $??4?? ?? $??* ??? $
??auto ?.+?9? $??Test Settings?? 9? ExpandDirection$
 Down=?$??0??.????  ?? Orientation$ Vertical=?.+??   ?? $
??auto ??? $
??auto ? ?? $
??auto ??? $
??auto ??? $
??auto ??? $
??auto ??? $
??auto ?.+??? $??0??$??0??$??Threshold : ??$??Center=?*  $MahApps.Metro.Controls.NumericUpDown  $??1??$??0??$	??100??   Interval$ 1Q?   Minimum$ 1Q?   Value ?? *Epoch+{??? *
IsTraining+ ?? 	Converter 2  ,JHoney_Flow.Converter.BooleanInvertConverter  ?? $??0??$??2??$??1??.+??? $??Train?? d? 	IsChecked ?? *UsingTrainSet+?? $??
Validation?? ?? *UsingValidationSet+?? $
??Test?? ?? *UsingTestSet+? $??1??  ?? $??* ??? $??* ?.+??? $??0??$
??Test??$??10q????? *CommandTest+{??? *
IsTraining+   ?? $??1??$
??Stop??$??10q????? *CommandStop+{??? *
IsTraining+? $??1??.+?,	   &JHoney_Flow.View.RepoLv2.ModelListView	   ?? *ModelListViewModel+