   M S B A M L   `   `   `  ����G  CChallengeENG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ChallengeENG.ViewModelsChallengeENG.ViewModels  5      !    ChallengeENG.Views.MainView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      <
viewModels%clr-namespace:ChallengeENG.ViewModels     5        � Title$  MainView��5      $	��410��5	      $	��418��5
      .��5      � $	��353��5   	   $��0,15,0,0q�5   	   $	��Top=�5   	    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� .+�5      �� $��64��5      $��64��5      $[��Upack://application:,,,/ChallengeENG;component/Resources/Icons/icons8-parameter-64.png��5      �� 5      $	��297��5      $��0,5,0,5q�5       $��Center=�5!      $��Center=�5"      $��10(�5#      $��Light"�5$      ���#2B2D42$h��byou must load parameters first in a valid view (3D Views, Floor Plans and Reflected Ceiling Plans)��5&       �� TextAlignment$ Center=�5'      $
��Wrap=�5(      �� 5)      $��0��5*      $	��150��5+      $��40��5,      $��	0,10,0,10q�5-      $��load parameters��5/      ��5.      �� *LoadParametersCommand+5/      50      �� 51   
   $��1��6   $
��0,10q�6"   .+�52      �� $	��297��53      $��0,0,0,5q�54      $��14(�55      $
��Bold"�56      ���#2B2D42$��Select parameter:��58      �� 59      $	��300��5:      $��Center=�5;       �� 
IsEditable$
 True��5<      ��5=      �� *ParameterNames+ �� SelectedItem 5>      �� *SelectedParameter+ �� UpdateSourceTrigger$ PropertyChanged=�5?      �� 5@   
   $��2��6   $
��0,10q�6"   .+�5A      �� $	��298��5B      $��0,0,0,5q�5C      $��14(�5D      $
��Bold"�5E      ���#2B2D42$��Select parameter value:��5G      �� 5H      $	��300��5I      $��Center=�5J      $
 True��5K      ��5L      �� *ParameterValues+ 5M      �� *SelectedParameterValue+$ PropertyChanged=�5N      �� 5O   
   $��3��5P      $
��0,10q�5Q      $��Center=�5R       �� Orientation$ 
Horizontal=�5S      .+�5T      �� $	��120��5U      $��40��5V      $��0,0,10,0q�5W      $��Isolate in View��5Y      ��5X      �� *IsolateViewCommand+5Y      �� 5Z      $	��120��5[      $��40��5\      $��10,0,0,0q�5]      $��Select��5_      ��5^      �� *SelectCommand+5_      5`      5a      5b      