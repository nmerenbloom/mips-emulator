﻿using MIPS_Emulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Mips mips;

		public MainWindow() {
			InitializeComponent();
			ProgramLoader loader = new ProgramLoader(new FileInfo("../../../projects/imem_test/imemtest.json"));
			mips = loader.Mips;
		}

		private void ViewImem(object sender, RoutedEventArgs e) {
			InstructionMemoryViewer imemWindow = new InstructionMemoryViewer(mips);
			imemWindow.Show();
			debuggerViews.Add(imemWindow);
		}

		private void ViewRegisters(object sender, RoutedEventArgs e) {
			RegistersViewer registersWindow = new RegistersViewer(mips.Reg);
			registersWindow.Show();
			debuggerViews.Add(registersWindow);
		}

		private void ViewVga(object sender, RoutedEventArgs e) {
			VgaDisplay vgaWindow = new VgaDisplay(mips);
			vgaWindow.Show();
			debuggerViews.Add(vgaWindow);
		}

		private void OpenAll(object sender, RoutedEventArgs e) {
			ViewImem(sender, e);
			ViewRegisters(sender, e);
			ViewVga(sender, e);
		}

		private void Step(object sender, RoutedEventArgs e) {
			mips.ExecuteNext();
			foreach (DebuggerView view in debuggerViews) {
				view.RefreshDisplay();
			}
		}
	    
		private void RunAll(object sender, RoutedEventArgs e) {
			Thread thread1 = new Thread(ExecuteAll);
			thread1.Start();
			Thread thread2 = new Thread(TickTimer);
			thread2.Start();
		}

		private void ExecuteAll() {
			while(true) {
				mips.ExecuteNext();
			}  
		}

		private void TickTimer() {
			Timer timer = new Timer((state) => TickAll(), "state", 0, 33);
			while(true);
		}
		
		private void TickAll() {
			this.Dispatcher.Invoke(() => {
				foreach (DebuggerView view in debuggerViews) {
					view.RefreshDisplay();
				}
			});
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
			Console.Beep();
			//TODO: Close all windows
		}
	}
}