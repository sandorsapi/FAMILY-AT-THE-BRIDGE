   M S B A M L   `   `   `  ����E  ABridge_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Bridge_App.ViewBridge_App.View  5      "    Bridge_App.View.OperatorView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Bridge_App.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      $	��300��5      $	��300��5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��5      $��5q�5        5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   $��0��5      $
��Left=�5      $��Select:��5       �� 5!      $��1��6   $��0��5"      $��People number:��5#      �� 5$      $��2��6   $��0��5%      $
��Left=�5&      $��Random time:��5'      y�5(      �� *RandomTextBoxVisibility+5)      � 5*   
   $��0��6   $��1��6     5+      �� 5,      $
��Auto �6    �� 5-      $
��Auto �6    �� 5.      $
��Auto �6     50      �� 51      $
��Auto �6#   �� 52      $��* �6#   .+�54      	� $��0��6   $��0��55      $��5q�56      $
��Left=�57      $��Center=�58      $��Random��59       d� 	IsChecked 5:      �� *RandomSelect+ �� Mode$ TwoWay=�	� 5<      $��0��6   $��1��5=      $��5q�5>      $��Right=�5?      $��Center=�5@      $��Manual��5A       5B      �� *ManualSelect+$ TwoWay=��� 5D      $��1��6   $��0��5E      $	��100��5F      $��5q�5G      $
��Left=�5H      |�5I      �� *PeopleNumberTextFocus+��5J      �� *PeopleNumber+$ TwoWay=��� 5L      $��1��6   $��1��5M      $��5q�5N      $	��Add��5P      ��5O      �� *ManualAddCommand+y�5Q      �� *AddButtonVisibility+�� 5R      $��2��6   $��0��5S      $	��100��5T      $��5q�5U      $
��Left=�5V      ��5W      �� *
RandomTime+$ TwoWay=�y�5Y      �� *RandomTextBoxVisibility+�� 5Z      $��2��6   $��1��5[      $��5q�5\      $��Run fill��5^      ��5]      �� *RandomAddCommand+y�5_      �� *RandomButtonVisibility+5`      �� 5c   
   $��3��6   $��0��5d      $��50��5e      $��5q�5f      $	��Top=�5g      $��People list::��5h      &   System.Windows.Controls.DataGrid  5i   
   $��3��6   $��1��5j      $	��165��5k      $	��200��5l      $��5q�5m      $
��Left=�5n      $	��Top=�5o         AutoGenerateColumns .  ��5q      �� *People+   Columns 5r      0  *System.Windows.Controls.DataGridTextColumn  5s      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6%      Header$
 Name��5u   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5t   %   �� *
peopleName+5u   %     5v      $ 80 6%   $
 Time��5x   %    5w   %   �� *	movedTime+5x   %   5z      5�      5�      