   M S B A M L   `   `   `  ????F  BJHoney_Flow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:JHoney_Flow.View.RepoLv2.RatioSetDialog'JHoney_Flow.View.RepoLv2.RatioSetDialog  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      @    :JHoney_Flow.View.RepoLv2.RatioSetDialog.RatioSetDialogView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:JHoney_Flow.View.RepoLv2.RatioSetDialog     5      H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5      S OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59'cmdhttp://www.codeplex.com/prism  5	      y?5
      ?? *
Visibility+.??5      ? 
???   ?.+?5   
   ? 
???????$??10q?6"     ? RowDefinitions  5      ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    .+?5      ? $??1??6    ? ColumnDefinitions 5      ?? 5      $??* ?6'   ?? 5      $
??auto ?6'   ?? 5      $
??auto ?6'     5      ?? 5      $??* ?6$   ?? 5      $
??auto ?6$   .+?5      ??  ?? Orientation$ 
Horizontal=?6   $??1??66   $??3??6F   $??0??6Z   $??5q?6g   .+?5       ? $??5q?6    5!      ?? 5"      $
??auto ?6/   ?? 5#      $
??auto ?6/     5%      ?? 5&      $
??auto ?6,   ?? 5'      $
??auto ?6,   .+?5)      ?? $??0??6$   $??2??64   $??0??6H   $??Center=?6?   $??Center=?6?   ??6U   ?? *TrainPercent+ ?? StringFormat$ {0:F1} %??6?   ?? 5*      $??0??6"   $??1??62    ?? 
IsReadOnly .  ?? TextAlignment$ Center=?6r   $??Center=?6?   $??Center=?6?   ??6Q   ?? *TrainImageCount+6?   *  $MahApps.Metro.Controls.NumericUpDown  5+      $??1??61   $??1??6A      Interval$	 0.1Q?6?      StringFormat$ F1??6?      Minimum$ 0Q?6?   	   Value	 6N   ?? *
TrainRatio+
 ?? UpdateSourceTrigger$
 PropertyChanged=?.  (System.Windows.Interactivity.Interaction   Triggers 5,      /  )System.Windows.Interactivity.EventTrigger  5-   "      	EventName$ ValueChanged??61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5.   &   -  'Prism.Interactivity.InvokeCommandAction     Command 6>   ?? *CommandRatioChanged+   CommandParameter 6f   ?? 5/   #   51      52      ? 53      $??5q?6    54      ?? 55      $
??auto ?6/   ?? 56      $
??auto ?6/     58      ?? 59      $
??auto ?6,   ?? 5:      $
??auto ?6,   .+?5<      ?? $??0??6$   $??2??64   $??0??6H   $??Center=?6?   $??Center=?6?   ??6U   ?? *ValidationPercent+$ {0:F1} %??6?   ?? 5=      $??0??6"   $??1??62    . ??6Q   ?? *ValidationImageCount+  5>      $??1??61   $??1??6A   $	 0.1Q?6?   $ F1??6?   $ 0Q?6?   	 6N   ?? *ValidationRatio+$
 PropertyChanged=? 5?        5@   "   $ ValueChanged??61   . 5A   &      6>   ?? *CommandRatioChanged+ 6f   ?? 5B   #   5D      5E      ? 5F      $??5q?6    5G      ?? 5H      $
??auto ?6/   ?? 5I      $
??auto ?6/     5K      ?? 5L      $
??auto ?6,   ?? 5M      $
??auto ?6,   .+?5O      ?? $??0??6$   $??2??64   $??0??6H   $??Center=?6?   $??Center=?6?   ??6U   ?? *TestPercent+$ {0:F1} %??6?   ?? 5P      $??0??6"   $??1??62    . ??6Q   ?? *TestImageCount+  5Q      $??1??61   $??1??6A   $	 0.1Q?6?   $ F1??6?   $ 0Q?6?   	 6N   ?? *	TestRatio+$
 PropertyChanged=? 5R        5S   "   $ ValueChanged??61   . 5T   &      6>   ?? *CommandRatioChanged+ 6f   ?? 5U   #   5W      5X      5Y      ?? 5[      $??1??6   $??1??6)   $??	Set Ratio??66   $??10q?6n   ??6J   ?? *CommandSetRatio+6n   ?? 5\      $??2??6   $??1??6)   $??Cancel??66   $??10q?6j   ??6G   ?? *CommandCancel+6j   5]      5^      5_      5`      