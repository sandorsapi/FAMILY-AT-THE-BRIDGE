   M S B A M L   `   `   `  ����E  ABridge_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Bridge_App.ViewBridge_App.View  5          Bridge_App.View.MenuView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Bridge_App.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $	��100��5      $��5q�5      $	��Run��5      $��2q�5      ��5      �� *
RunCommand+{�5      �� *
RunEnabled+5      �� 5   
   $��1��6   $	��100��5      $��5q�5      $
��Stop��5      $��2q�5      ��5      �� *StopCommand+{�5      �� *StopEnabled+5      �� 5   
   $��2��6   $��5q�5      $��Right=�5       $��Center=�5!      $��Status��5"      � 5#   
   $��3��6   $	��300��5$      $��30��5%      $��5q�5&      $��Right=�5'      $��Center=�5(       � Minimum$ 1Q�5*       � Maximum 5)      �� *ProgressBarMax+ � Value 5+      �� *ProgressValue+ �� UpdateSourceTrigger$ PropertyChanged=�5/      50      