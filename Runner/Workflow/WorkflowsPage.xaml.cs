﻿using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace Runner.Workflows
{
    /// <summary>
    /// Логика взаимодействия для WorkflowsPage.xaml
    /// </summary>
    public partial class WorkflowsPage : Page
    {
        public event EventHandler<TaskConfig> TaskAdded;
        public event EventHandler<WorkflowStep> TaskRemoved;
        public WorkflowsPage(WorkflowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            TaskAdded += vm.OnTaskAddedHandler;
            TaskRemoved += vm.OnTaskRemovedHandler;
            
        }
        private void TaskList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBox sourceList = (ListBox)(sender);
            if (sourceList.SelectedItem != null)
            {
                DataObject item = new DataObject();
                item.SetData("task", (TaskConfig)sourceList.SelectedItem);
                DragDrop.DoDragDrop(sourceList, item, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        private void OnTaskAdded(TaskConfig task)
        {
            TaskAdded(this, task);
        }

        private void RemoveTask_From_Workflow(object sender, MouseButtonEventArgs args)
        {
            if (args.LeftButton != MouseButtonState.Pressed) return;
            var item = sender as FrameworkElement;
            if (item == null) return;
            var step = item.DataContext as WorkflowStep;
            if (step == null) return;
            OnTaskRemoved(step);
            args.Handled = true;
        }
        private void TaskButton_DoubleClick(object sender, MouseButtonEventArgs args)
        {
            if (args.LeftButton != MouseButtonState.Pressed) return;
            var button = (Button)sender;
            StackPanel panel = button.Parent as StackPanel;
            if (panel != null)
            {
                panel.Children.Remove(button);
            }
        }
        private void TaskList_DoubleClick(object sender, MouseButtonEventArgs args)
        {
            if(args.LeftButton != MouseButtonState.Pressed) return;
            var item = sender as FrameworkElement;
            if (item == null) return;
            var task = item.DataContext as TaskConfig;
            if (task == null) return;
            OnTaskAdded(task);
            args.Handled = true;
        }
        private void OnTaskRemoved(WorkflowStep task)
        {
            TaskRemoved(this, task);
        }
    }
}
