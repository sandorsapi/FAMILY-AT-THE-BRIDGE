   M S B A M L   `   `   `  ����E  ABridge_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Bridge_App
Bridge_App  5   	   1clr-namespace:Bridge_App.ViewBridge_App.View  5   	       Bridge_App.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   *localclr-namespace:Bridge_App     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .viewclr-namespace:Bridge_App.View     5   	     � Title$  FAMILY AT THE BRIDGE��5   	   $
��1200��5	   	   $	��500��5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
      Bridge_App.View.MenuView  $��0��6    � DataContext 6%   �� *ContentMenuViewModel+� 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      "   Bridge_App.View.OperatorView  $��0��6     60   �� *ContentOperatorViewModel+�� 5      -   5         ��Name$ gridSplitter��5      $��1��5      $��5��5      $	��500��5      $��5q�5      $
��Left=�5       $	��Top=�5!      2   ,Bridge_App.View.InformationInterfaceViewxaml  5"      $��2��60    6@   �� *&$ContentInformationInterfaceViewModel+5#      5$      5%      