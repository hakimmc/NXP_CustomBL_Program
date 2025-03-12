﻿namespace NXP_OttoBugger
{
    partial class OttobuggerV3
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SwUpdate_ProgressBar = new ProgressBar();
            UartRadio = new RadioButton();
            COMM_MODE_GB = new GroupBox();
            CanRadio = new RadioButton();
            BAUD_GB = new GroupBox();
            BaudCombobox = new ComboBox();
            ConnectButton = new Button();
            CFG_GB = new GroupBox();
            OpenTest_Window_Button = new Button();
            UART_COM_GB = new GroupBox();
            UartComportCombobox = new ComboBox();
            select_file_button = new Button();
            filename_label = new Label();
            Sw_UpdateStartButton = new Button();
            SW_UPD_GB = new GroupBox();
            Sw_DuringTimeLabel = new Label();
            TEST_GB = new GroupBox();
            SendData = new Button();
            UART_TEST_GB = new GroupBox();
            label15 = new Label();
            UartDatasRX = new TextBox();
            label14 = new Label();
            UartDatasTX = new TextBox();
            CAN_TEST_GB = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            CanDatasRXD8 = new TextBox();
            CanDatasTXD8 = new TextBox();
            CanDatasRXD7 = new TextBox();
            CanDatasTXD7 = new TextBox();
            CanDatasRXD6 = new TextBox();
            CanDatasTXD6 = new TextBox();
            CanDatasRXD5 = new TextBox();
            CanDatasTXD5 = new TextBox();
            CanDatasRXD4 = new TextBox();
            CanDatasTXD4 = new TextBox();
            label6 = new Label();
            label5 = new Label();
            CanDatasRXD3 = new TextBox();
            CanDatasTXD3 = new TextBox();
            CanDatasRXD2 = new TextBox();
            CanDatasTXD2 = new TextBox();
            CanDatasRXD1 = new TextBox();
            CanDatasTXD1 = new TextBox();
            CanDatasRXDLC = new TextBox();
            CanDatasTXDLC = new TextBox();
            label4 = new Label();
            CanDatasRXID = new TextBox();
            label3 = new Label();
            CanDatasTXID = new TextBox();
            label2 = new Label();
            COMM_MODE_GB.SuspendLayout();
            BAUD_GB.SuspendLayout();
            CFG_GB.SuspendLayout();
            UART_COM_GB.SuspendLayout();
            SW_UPD_GB.SuspendLayout();
            TEST_GB.SuspendLayout();
            UART_TEST_GB.SuspendLayout();
            CAN_TEST_GB.SuspendLayout();
            SuspendLayout();
            // 
            // SwUpdate_ProgressBar
            // 
            SwUpdate_ProgressBar.Location = new Point(6, 109);
            SwUpdate_ProgressBar.Name = "SwUpdate_ProgressBar";
            SwUpdate_ProgressBar.Size = new Size(230, 23);
            SwUpdate_ProgressBar.TabIndex = 0;
            // 
            // UartRadio
            // 
            UartRadio.AutoSize = true;
            UartRadio.Location = new Point(6, 22);
            UartRadio.Name = "UartRadio";
            UartRadio.Size = new Size(47, 19);
            UartRadio.TabIndex = 3;
            UartRadio.TabStop = true;
            UartRadio.Text = "Uart";
            UartRadio.UseVisualStyleBackColor = true;
            UartRadio.CheckedChanged += UartRadio_CheckedChanged;
            // 
            // COMM_MODE_GB
            // 
            COMM_MODE_GB.Controls.Add(CanRadio);
            COMM_MODE_GB.Controls.Add(UartRadio);
            COMM_MODE_GB.Location = new Point(6, 17);
            COMM_MODE_GB.Name = "COMM_MODE_GB";
            COMM_MODE_GB.Size = new Size(111, 58);
            COMM_MODE_GB.TabIndex = 4;
            COMM_MODE_GB.TabStop = false;
            COMM_MODE_GB.Text = "Comm Mode";
            // 
            // CanRadio
            // 
            CanRadio.AutoSize = true;
            CanRadio.Location = new Point(59, 22);
            CanRadio.Name = "CanRadio";
            CanRadio.Size = new Size(46, 19);
            CanRadio.TabIndex = 4;
            CanRadio.TabStop = true;
            CanRadio.Text = "Can";
            CanRadio.UseVisualStyleBackColor = true;
            // 
            // BAUD_GB
            // 
            BAUD_GB.Controls.Add(BaudCombobox);
            BAUD_GB.Location = new Point(6, 81);
            BAUD_GB.Name = "BAUD_GB";
            BAUD_GB.Size = new Size(105, 65);
            BAUD_GB.TabIndex = 5;
            BAUD_GB.TabStop = false;
            BAUD_GB.Text = "Baud";
            // 
            // BaudCombobox
            // 
            BaudCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            BaudCombobox.FormattingEnabled = true;
            BaudCombobox.Location = new Point(6, 22);
            BaudCombobox.Name = "BaudCombobox";
            BaudCombobox.Size = new Size(93, 23);
            BaudCombobox.TabIndex = 0;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(6, 152);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(231, 23);
            ConnectButton.TabIndex = 6;
            ConnectButton.Text = "Connect to Device";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // CFG_GB
            // 
            CFG_GB.Controls.Add(OpenTest_Window_Button);
            CFG_GB.Controls.Add(UART_COM_GB);
            CFG_GB.Controls.Add(ConnectButton);
            CFG_GB.Controls.Add(BAUD_GB);
            CFG_GB.Controls.Add(COMM_MODE_GB);
            CFG_GB.Location = new Point(12, 12);
            CFG_GB.Name = "CFG_GB";
            CFG_GB.Size = new Size(244, 181);
            CFG_GB.TabIndex = 7;
            CFG_GB.TabStop = false;
            CFG_GB.Text = "Configuration";
            // 
            // OpenTest_Window_Button
            // 
            OpenTest_Window_Button.Location = new Point(123, 17);
            OpenTest_Window_Button.Name = "OpenTest_Window_Button";
            OpenTest_Window_Button.Size = new Size(114, 58);
            OpenTest_Window_Button.TabIndex = 7;
            OpenTest_Window_Button.Text = "Open Test Window";
            OpenTest_Window_Button.UseVisualStyleBackColor = true;
            OpenTest_Window_Button.Click += OpenTest_Window_Button_Click;
            // 
            // UART_COM_GB
            // 
            UART_COM_GB.Controls.Add(UartComportCombobox);
            UART_COM_GB.Location = new Point(117, 81);
            UART_COM_GB.Name = "UART_COM_GB";
            UART_COM_GB.Size = new Size(120, 65);
            UART_COM_GB.TabIndex = 6;
            UART_COM_GB.TabStop = false;
            UART_COM_GB.Text = "Uart Com Port";
            // 
            // UartComportCombobox
            // 
            UartComportCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            UartComportCombobox.FormattingEnabled = true;
            UartComportCombobox.Location = new Point(6, 22);
            UartComportCombobox.Name = "UartComportCombobox";
            UartComportCombobox.Size = new Size(104, 23);
            UartComportCombobox.TabIndex = 1;
            // 
            // select_file_button
            // 
            select_file_button.Location = new Point(6, 22);
            select_file_button.Name = "select_file_button";
            select_file_button.Size = new Size(231, 23);
            select_file_button.TabIndex = 7;
            select_file_button.Text = "Select File";
            select_file_button.UseVisualStyleBackColor = true;
            // 
            // filename_label
            // 
            filename_label.AutoSize = true;
            filename_label.Location = new Point(6, 48);
            filename_label.Name = "filename_label";
            filename_label.Size = new Size(61, 15);
            filename_label.TabIndex = 8;
            filename_label.Text = "Filename :";
            // 
            // Sw_UpdateStartButton
            // 
            Sw_UpdateStartButton.Location = new Point(6, 66);
            Sw_UpdateStartButton.Name = "Sw_UpdateStartButton";
            Sw_UpdateStartButton.Size = new Size(231, 23);
            Sw_UpdateStartButton.TabIndex = 9;
            Sw_UpdateStartButton.Text = "Software Update Start";
            Sw_UpdateStartButton.UseVisualStyleBackColor = true;
            // 
            // SW_UPD_GB
            // 
            SW_UPD_GB.Controls.Add(Sw_DuringTimeLabel);
            SW_UPD_GB.Controls.Add(select_file_button);
            SW_UPD_GB.Controls.Add(filename_label);
            SW_UPD_GB.Controls.Add(Sw_UpdateStartButton);
            SW_UPD_GB.Controls.Add(SwUpdate_ProgressBar);
            SW_UPD_GB.Location = new Point(12, 199);
            SW_UPD_GB.Name = "SW_UPD_GB";
            SW_UPD_GB.Size = new Size(244, 143);
            SW_UPD_GB.TabIndex = 10;
            SW_UPD_GB.TabStop = false;
            SW_UPD_GB.Text = "Software Update";
            // 
            // Sw_DuringTimeLabel
            // 
            Sw_DuringTimeLabel.AutoSize = true;
            Sw_DuringTimeLabel.Location = new Point(6, 91);
            Sw_DuringTimeLabel.Name = "Sw_DuringTimeLabel";
            Sw_DuringTimeLabel.Size = new Size(39, 15);
            Sw_DuringTimeLabel.TabIndex = 10;
            Sw_DuringTimeLabel.Text = "Time :";
            // 
            // TEST_GB
            // 
            TEST_GB.Controls.Add(SendData);
            TEST_GB.Controls.Add(UART_TEST_GB);
            TEST_GB.Controls.Add(CAN_TEST_GB);
            TEST_GB.Location = new Point(270, 7);
            TEST_GB.Name = "TEST_GB";
            TEST_GB.Size = new Size(377, 335);
            TEST_GB.TabIndex = 11;
            TEST_GB.TabStop = false;
            TEST_GB.Text = "Test Messages";
            // 
            // SendData
            // 
            SendData.Location = new Point(6, 301);
            SendData.Name = "SendData";
            SendData.Size = new Size(363, 23);
            SendData.TabIndex = 9;
            SendData.Text = "Send Data";
            SendData.UseVisualStyleBackColor = true;
            // 
            // UART_TEST_GB
            // 
            UART_TEST_GB.Controls.Add(label15);
            UART_TEST_GB.Controls.Add(UartDatasRX);
            UART_TEST_GB.Controls.Add(label14);
            UART_TEST_GB.Controls.Add(UartDatasTX);
            UART_TEST_GB.Location = new Point(6, 140);
            UART_TEST_GB.Name = "UART_TEST_GB";
            UART_TEST_GB.Size = new Size(363, 136);
            UART_TEST_GB.TabIndex = 1;
            UART_TEST_GB.TabStop = false;
            UART_TEST_GB.Text = "Uart Datas";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(9, 54);
            label15.Name = "label15";
            label15.Size = new Size(21, 15);
            label15.TabIndex = 32;
            label15.Text = "RX";
            // 
            // UartDatasRX
            // 
            UartDatasRX.Location = new Point(32, 51);
            UartDatasRX.Multiline = true;
            UartDatasRX.Name = "UartDatasRX";
            UartDatasRX.Size = new Size(321, 69);
            UartDatasRX.TabIndex = 33;
            UartDatasRX.Text = "UartExampleRxData";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(10, 25);
            label14.Name = "label14";
            label14.Size = new Size(20, 15);
            label14.TabIndex = 32;
            label14.Text = "TX";
            // 
            // UartDatasTX
            // 
            UartDatasTX.Location = new Point(32, 22);
            UartDatasTX.Name = "UartDatasTX";
            UartDatasTX.Size = new Size(321, 23);
            UartDatasTX.TabIndex = 0;
            UartDatasTX.Text = "UartExampleTxData";
            // 
            // CAN_TEST_GB
            // 
            CAN_TEST_GB.Controls.Add(label13);
            CAN_TEST_GB.Controls.Add(label12);
            CAN_TEST_GB.Controls.Add(label11);
            CAN_TEST_GB.Controls.Add(label10);
            CAN_TEST_GB.Controls.Add(label9);
            CAN_TEST_GB.Controls.Add(label8);
            CAN_TEST_GB.Controls.Add(label7);
            CAN_TEST_GB.Controls.Add(CanDatasRXD8);
            CAN_TEST_GB.Controls.Add(CanDatasTXD8);
            CAN_TEST_GB.Controls.Add(CanDatasRXD7);
            CAN_TEST_GB.Controls.Add(CanDatasTXD7);
            CAN_TEST_GB.Controls.Add(CanDatasRXD6);
            CAN_TEST_GB.Controls.Add(CanDatasTXD6);
            CAN_TEST_GB.Controls.Add(CanDatasRXD5);
            CAN_TEST_GB.Controls.Add(CanDatasTXD5);
            CAN_TEST_GB.Controls.Add(CanDatasRXD4);
            CAN_TEST_GB.Controls.Add(CanDatasTXD4);
            CAN_TEST_GB.Controls.Add(label6);
            CAN_TEST_GB.Controls.Add(label5);
            CAN_TEST_GB.Controls.Add(CanDatasRXD3);
            CAN_TEST_GB.Controls.Add(CanDatasTXD3);
            CAN_TEST_GB.Controls.Add(CanDatasRXD2);
            CAN_TEST_GB.Controls.Add(CanDatasTXD2);
            CAN_TEST_GB.Controls.Add(CanDatasRXD1);
            CAN_TEST_GB.Controls.Add(CanDatasTXD1);
            CAN_TEST_GB.Controls.Add(CanDatasRXDLC);
            CAN_TEST_GB.Controls.Add(CanDatasTXDLC);
            CAN_TEST_GB.Controls.Add(label4);
            CAN_TEST_GB.Controls.Add(CanDatasRXID);
            CAN_TEST_GB.Controls.Add(label3);
            CAN_TEST_GB.Controls.Add(CanDatasTXID);
            CAN_TEST_GB.Controls.Add(label2);
            CAN_TEST_GB.Location = new Point(6, 22);
            CAN_TEST_GB.Name = "CAN_TEST_GB";
            CAN_TEST_GB.Size = new Size(363, 112);
            CAN_TEST_GB.TabIndex = 0;
            CAN_TEST_GB.TabStop = false;
            CAN_TEST_GB.Text = "Can Hex Datas ";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(330, 76);
            label13.Name = "label13";
            label13.Size = new Size(21, 15);
            label13.TabIndex = 31;
            label13.Text = "D8";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(304, 76);
            label12.Name = "label12";
            label12.Size = new Size(21, 15);
            label12.TabIndex = 30;
            label12.Text = "D7";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(277, 76);
            label11.Name = "label11";
            label11.Size = new Size(21, 15);
            label11.TabIndex = 29;
            label11.Text = "D6";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(250, 76);
            label10.Name = "label10";
            label10.Size = new Size(21, 15);
            label10.TabIndex = 28;
            label10.Text = "D5";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(223, 76);
            label9.Name = "label9";
            label9.Size = new Size(21, 15);
            label9.TabIndex = 27;
            label9.Text = "D4";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(195, 76);
            label8.Name = "label8";
            label8.Size = new Size(21, 15);
            label8.TabIndex = 26;
            label8.Text = "D3";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(167, 76);
            label7.Name = "label7";
            label7.Size = new Size(21, 15);
            label7.TabIndex = 25;
            label7.Text = "D2";
            // 
            // CanDatasRXD8
            // 
            CanDatasRXD8.Location = new Point(330, 51);
            CanDatasRXD8.MaxLength = 2;
            CanDatasRXD8.Name = "CanDatasRXD8";
            CanDatasRXD8.Size = new Size(23, 23);
            CanDatasRXD8.TabIndex = 24;
            CanDatasRXD8.Text = "18";
            // 
            // CanDatasTXD8
            // 
            CanDatasTXD8.Location = new Point(330, 29);
            CanDatasTXD8.MaxLength = 2;
            CanDatasTXD8.Name = "CanDatasTXD8";
            CanDatasTXD8.Size = new Size(23, 23);
            CanDatasTXD8.TabIndex = 23;
            CanDatasTXD8.Text = "18";
            // 
            // CanDatasRXD7
            // 
            CanDatasRXD7.Location = new Point(303, 51);
            CanDatasRXD7.MaxLength = 2;
            CanDatasRXD7.Name = "CanDatasRXD7";
            CanDatasRXD7.Size = new Size(23, 23);
            CanDatasRXD7.TabIndex = 22;
            CanDatasRXD7.Text = "17";
            // 
            // CanDatasTXD7
            // 
            CanDatasTXD7.Location = new Point(303, 29);
            CanDatasTXD7.MaxLength = 2;
            CanDatasTXD7.Name = "CanDatasTXD7";
            CanDatasTXD7.Size = new Size(23, 23);
            CanDatasTXD7.TabIndex = 21;
            CanDatasTXD7.Text = "17";
            // 
            // CanDatasRXD6
            // 
            CanDatasRXD6.Location = new Point(276, 51);
            CanDatasRXD6.MaxLength = 2;
            CanDatasRXD6.Name = "CanDatasRXD6";
            CanDatasRXD6.Size = new Size(23, 23);
            CanDatasRXD6.TabIndex = 20;
            CanDatasRXD6.Text = "16";
            // 
            // CanDatasTXD6
            // 
            CanDatasTXD6.Location = new Point(276, 29);
            CanDatasTXD6.MaxLength = 2;
            CanDatasTXD6.Name = "CanDatasTXD6";
            CanDatasTXD6.Size = new Size(23, 23);
            CanDatasTXD6.TabIndex = 19;
            CanDatasTXD6.Text = "16";
            // 
            // CanDatasRXD5
            // 
            CanDatasRXD5.Location = new Point(249, 51);
            CanDatasRXD5.MaxLength = 2;
            CanDatasRXD5.Name = "CanDatasRXD5";
            CanDatasRXD5.Size = new Size(23, 23);
            CanDatasRXD5.TabIndex = 18;
            CanDatasRXD5.Text = "15";
            // 
            // CanDatasTXD5
            // 
            CanDatasTXD5.Location = new Point(249, 29);
            CanDatasTXD5.MaxLength = 2;
            CanDatasTXD5.Name = "CanDatasTXD5";
            CanDatasTXD5.Size = new Size(23, 23);
            CanDatasTXD5.TabIndex = 17;
            CanDatasTXD5.Text = "15";
            // 
            // CanDatasRXD4
            // 
            CanDatasRXD4.Location = new Point(222, 51);
            CanDatasRXD4.MaxLength = 2;
            CanDatasRXD4.Name = "CanDatasRXD4";
            CanDatasRXD4.Size = new Size(23, 23);
            CanDatasRXD4.TabIndex = 16;
            CanDatasRXD4.Text = "14";
            // 
            // CanDatasTXD4
            // 
            CanDatasTXD4.Location = new Point(222, 29);
            CanDatasTXD4.MaxLength = 2;
            CanDatasTXD4.Name = "CanDatasTXD4";
            CanDatasTXD4.Size = new Size(23, 23);
            CanDatasTXD4.TabIndex = 15;
            CanDatasTXD4.Text = "14";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(139, 76);
            label6.Name = "label6";
            label6.Size = new Size(21, 15);
            label6.TabIndex = 14;
            label6.Text = "D1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(99, 76);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 13;
            label5.Text = "DLC";
            // 
            // CanDatasRXD3
            // 
            CanDatasRXD3.Location = new Point(194, 51);
            CanDatasRXD3.MaxLength = 2;
            CanDatasRXD3.Name = "CanDatasRXD3";
            CanDatasRXD3.Size = new Size(23, 23);
            CanDatasRXD3.TabIndex = 12;
            CanDatasRXD3.Text = "13";
            // 
            // CanDatasTXD3
            // 
            CanDatasTXD3.Location = new Point(194, 29);
            CanDatasTXD3.MaxLength = 2;
            CanDatasTXD3.Name = "CanDatasTXD3";
            CanDatasTXD3.Size = new Size(23, 23);
            CanDatasTXD3.TabIndex = 11;
            CanDatasTXD3.Text = "13";
            // 
            // CanDatasRXD2
            // 
            CanDatasRXD2.Location = new Point(166, 51);
            CanDatasRXD2.MaxLength = 2;
            CanDatasRXD2.Name = "CanDatasRXD2";
            CanDatasRXD2.Size = new Size(23, 23);
            CanDatasRXD2.TabIndex = 10;
            CanDatasRXD2.Text = "12";
            // 
            // CanDatasTXD2
            // 
            CanDatasTXD2.Location = new Point(166, 29);
            CanDatasTXD2.MaxLength = 2;
            CanDatasTXD2.Name = "CanDatasTXD2";
            CanDatasTXD2.Size = new Size(23, 23);
            CanDatasTXD2.TabIndex = 9;
            CanDatasTXD2.Text = "12";
            // 
            // CanDatasRXD1
            // 
            CanDatasRXD1.Location = new Point(138, 51);
            CanDatasRXD1.MaxLength = 2;
            CanDatasRXD1.Name = "CanDatasRXD1";
            CanDatasRXD1.Size = new Size(23, 23);
            CanDatasRXD1.TabIndex = 8;
            CanDatasRXD1.Text = "11";
            // 
            // CanDatasTXD1
            // 
            CanDatasTXD1.Location = new Point(138, 29);
            CanDatasTXD1.MaxLength = 2;
            CanDatasTXD1.Name = "CanDatasTXD1";
            CanDatasTXD1.Size = new Size(23, 23);
            CanDatasTXD1.TabIndex = 7;
            CanDatasTXD1.Text = "11";
            // 
            // CanDatasRXDLC
            // 
            CanDatasRXDLC.Location = new Point(106, 51);
            CanDatasRXDLC.MaxLength = 2;
            CanDatasRXDLC.Name = "CanDatasRXDLC";
            CanDatasRXDLC.Size = new Size(15, 23);
            CanDatasRXDLC.TabIndex = 6;
            CanDatasRXDLC.Text = "8";
            // 
            // CanDatasTXDLC
            // 
            CanDatasTXDLC.Location = new Point(106, 29);
            CanDatasTXDLC.MaxLength = 2;
            CanDatasTXDLC.Name = "CanDatasTXDLC";
            CanDatasTXDLC.Size = new Size(15, 23);
            CanDatasTXDLC.TabIndex = 5;
            CanDatasTXDLC.Text = "8";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 76);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 4;
            label4.Text = "EXT/ STD ID";
            // 
            // CanDatasRXID
            // 
            CanDatasRXID.Location = new Point(32, 51);
            CanDatasRXID.MaxLength = 8;
            CanDatasRXID.Name = "CanDatasRXID";
            CanDatasRXID.Size = new Size(60, 23);
            CanDatasRXID.TabIndex = 3;
            CanDatasRXID.Text = "20000000";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 55);
            label3.Name = "label3";
            label3.Size = new Size(21, 15);
            label3.TabIndex = 2;
            label3.Text = "RX";
            // 
            // CanDatasTXID
            // 
            CanDatasTXID.Location = new Point(32, 29);
            CanDatasTXID.MaxLength = 8;
            CanDatasTXID.Name = "CanDatasTXID";
            CanDatasTXID.Size = new Size(60, 23);
            CanDatasTXID.TabIndex = 1;
            CanDatasTXID.Text = "20000000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 33);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 0;
            label2.Text = "TX";
            // 
            // OttobuggerV3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(655, 351);
            Controls.Add(TEST_GB);
            Controls.Add(SW_UPD_GB);
            Controls.Add(CFG_GB);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "OttobuggerV3";
            Text = "OttoBugger";
            FormClosing += OttobuggerV3_FormClosing;
            Load += OttobuggerV3_Load;
            MouseDoubleClick += OttobuggerV3_MouseDoubleClick;
            COMM_MODE_GB.ResumeLayout(false);
            COMM_MODE_GB.PerformLayout();
            BAUD_GB.ResumeLayout(false);
            CFG_GB.ResumeLayout(false);
            UART_COM_GB.ResumeLayout(false);
            SW_UPD_GB.ResumeLayout(false);
            SW_UPD_GB.PerformLayout();
            TEST_GB.ResumeLayout(false);
            UART_TEST_GB.ResumeLayout(false);
            UART_TEST_GB.PerformLayout();
            CAN_TEST_GB.ResumeLayout(false);
            CAN_TEST_GB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        public static Button select_cfg_file_button;
        public static Label cfgfilename_label;
        public ProgressBar SwUpdate_ProgressBar;
        public RadioButton UartRadio;
        public GroupBox COMM_MODE_GB;
        public RadioButton CanRadio;
        public GroupBox BAUD_GB;
        public ComboBox BaudCombobox;
        public Button ConnectButton;
        public GroupBox CFG_GB;
        public Button select_file_button;
        public Label filename_label;
        public Button Sw_UpdateStartButton;
        public GroupBox SW_UPD_GB;
        public Label Sw_DuringTimeLabel;
        public GroupBox TEST_GB;
        public GroupBox UART_TEST_GB;
        public GroupBox CAN_TEST_GB;
        public Label label4;
        public TextBox CanDatasRXID;
        public Label label3;
        public TextBox CanDatasTXID;
        public Label label2;
        public Label label7;
        public TextBox CanDatasRXD8;
        public TextBox CanDatasTXD8;
        public TextBox CanDatasRXD7;
        public TextBox CanDatasTXD7;
        public TextBox CanDatasRXD6;
        public TextBox CanDatasTXD6;
        public TextBox CanDatasRXD5;
        public TextBox CanDatasTXD5;
        public TextBox CanDatasRXD4;
        public TextBox CanDatasTXD4;
        public Label label6;
        public Label label5;
        public TextBox CanDatasRXD3;
        public TextBox CanDatasTXD3;
        public TextBox CanDatasRXD2;
        public TextBox CanDatasTXD2;
        public TextBox CanDatasRXD1;
        public TextBox CanDatasTXD1;
        public TextBox CanDatasRXDLC;
        public TextBox CanDatasTXDLC;
        public Label label10;
        public Label label9;
        public Label label8;
        public GroupBox UART_COM_GB;
        public ComboBox UartComportCombobox;
        public Label label13;
        public Label label12;
        public Label label11;
        public Button SendData;
        public Label label15;
        public TextBox UartDatasRX;
        public Label label14;
        public TextBox UartDatasTX;
        public Button OpenTest_Window_Button;
    }
}
