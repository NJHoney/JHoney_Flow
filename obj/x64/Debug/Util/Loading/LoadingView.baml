   M S B A M L   `   `   `  ????F  BJHoney_Flow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:JHoney_Flow.Util.LoadingJHoney_Flow.Util.Loading  5      *    $JHoney_Flow.Util.Loading.LoadingView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:JHoney_Flow.Util.Loading     5      H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      y?5      ?? *
Visibility+.??5      ?   ? ColumnDefinitions  5   
   ?? 5      $??* ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6   ?? 5      $
??auto ?6   .+?5   
   ? 
???   ?$??1??6"   $??1??62   .+?5      ? $??10q?6   
???????  5      ?? 5      $
??auto ?6'   ?? 5      $??* ?6'    5      ?? 5      $
??auto ?6$   ?? 5       $
??auto ?6$   ?? 5!      $
??auto ?6$   .+?5$      )  #MahApps.Metro.Controls.ProgressRing  $??0??6(   $??0??68   $??3??6E   $??20q?6V   ?? 5&      $??1??6   $??0??6,   $??Loading??69   $
??Left=?6H   $??Center=?6c   $??20(?6~   $??	ExtraBold"?6?   $??10q?6?   ?? 5(       ?? Orientation$ 
Horizontal=?6   $??1??66   $??1??6F   $??Right=?6S   $??5q?6o   .+?5)      ?? $??( ??6    
???   ?6*   ?? 5*      
???   ???6    ?? *Value+ ?? ElementName$ ProgressBar??6P   ?? 5+      $	?? / ??6    
???   ?6+   ?? 5,      
???   ???6    ?? *	Maximum+$ ProgressBar??6R   ?? 5-      $	?? ) ??6    
???   ?6+   5.      ? 50      -   5         ?Name$ ProgressBar??50      $??1??63   $??2??6C   $??5q?6P   $	??150??6[    ? Minimum 6g   ?? *ProgressMin+ ? Maximum 6?   ?? *ProgressMax+ ? Value 6?   ?? *ProgressCurrent+51      52      53      54      