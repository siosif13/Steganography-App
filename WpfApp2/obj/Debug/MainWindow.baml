   M S B A M L   `   `   `  ����\  XSteganographyApp_IosifStefan_2019, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SteganographyAppSteganographyApp  5   	   !    SteganographyApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SteganographyApp     5   	     � Title$1  +Steganography App           © Iosif Stefan��5   	   $	��638��6A   $��656.5��6N    � 
ResizeMode$ NoResize=�6\   .��5	      � $
��Left=�6   $	��600��6&   $��10,10,-6,-1q�63   $	��Top=�6H   $	��630��6`    � RowDefinitions 5
   
   �� 5      $
��535* �6   �� 5      $	��69* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $
��105* �6   �� 5      $	��67* �6   .+�5   
   �� -   5         ��Name$ ImageBox��5      $��	0,0,10,94q�6"   $��3��65   �� 5   
   -   5         ��Name$ TextBox��5      $
��Left=�6#   $	��116��6>   $��
10,457,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $��Enter text here...��6s   $	��Top=�6�   $	��459��6�   $��14(�6�   $��3��6�   $��2��6�   6�   �� 5   
   -   5         ��Name$ DecryptButton��5      $��Decrypt��6(   $
��Left=�6:   $��93,7,0,0q�6U   $	��Top=�6g   $	��128��6   $��35                 ��6�   $��12(�5      $��2��6!   $��1��61   6>   �� 5   
   -   5        $ EncryptButton��5      $��Encrypt��6(   $
��Left=�6:   $��
93,498,0,0q�6U   $	��Top=�6i   $	��128��6�   $��35                 ��6�   $��12(�5      $��2��6!   $��2��61   6B   �� 5   
   -   5        $ 
ChooseFile��5      $��	Browse...��6%   $
��Left=�69   $��
93,458,0,0q�6T   $	��Top=�6h   $	��128��6�   $��35                 ��6�   $��12(�5      $��2��6:   5      5      