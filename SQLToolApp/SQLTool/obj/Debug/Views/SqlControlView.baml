   M S B A M L   `   `   `  ����B  >SQLTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:SQLTool.ViewModelsSQLTool.ViewModels  5   P   "    SQLTool.Views.SqlControlView   5      a ]DevExpress.Xpf.Core.v18.2, Version=18.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YDevExpress.Mvvm.v18.2, Version=18.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      3
ViewModels clr-namespace:SQLTool.ViewModels   6P   .��5	      �� -   5          ��Name$  
pnlControl��5	       � 	Resources 5
   
    �� 
TargetType �� Property �� Value)  #DevExpress.Xpf.Editors.ComboBoxEdit��%�   '��       '  H     �� 5      " ��6   .�5      ��  � ��$ 15(�6-   5      �� 5      "  6   .�5      ��  � ��$ 15(�6-   5      5      .+�5   
   �  � ColumnDefinitions 5      �� 5      $��70 �6#   �� 5      .+�5      �� $��0��6   .��6$   
Sql Type6.     5      -   5          Name$ 
cboSqlType��5   /   $��1��6   +  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource 6C   �� *
lstSqlType+2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5      '  !DevExpress.Mvvm.UI.EventToCommand  5      �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]	   	EventName$	 EditValueChanged��6,   +  %DevExpress.Mvvm.UI.EventToCommandBase
   UseDispatcher$

 TrueX�6�      Command 6I   �� *selectedItemChangeCommand+   CommandParameter 6w   ��  �� ElementName$ 
cboSqlType�� �� Path$ 	EditValue�6�   5      5      � 5   
    5      �� 5       $��70 �6#   �� 5!      .+�5#      �� $��0��6   .��6$   Server6,     5$      -   5        $ 	cboServer��5$   /   $��1��6    6B   �� *
lstServers+ 5%        5&      $	 SelectedIndexChanged��6,   $

 TrueX�6�    6M   �� *!serverSelectedItemChangeCommand+ 6�   �� $ 	cboServer��$ SelectedIndex�5(      5*      5+      � 5,   
    5-      �� 5.      $��70 �6#   �� 5/      .+�51      �� $��0��6   .��6$   
Database6.     52      -   5        $ cboDatabase��52   /   $��1��6      DisplayMember$
 name��6h      ValueMember$
 name��6}    6D   �� *lstDatabase+ 53        54      $	 EditValueChanged��6,   $

 TrueX�6�    6I   �� *selectedDatabaseChangeCommand+ 6{   �� $ cboDatabase��$ 	EditValue�6�   56      57      58      59      