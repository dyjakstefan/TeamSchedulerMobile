﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.ViewModels.TeamVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.TeamPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTeamPage : ContentPage
    {
        private EditTeamViewModel viewModel;

        public EditTeamPage(Team team)
        {
            InitializeComponent();
            viewModel = new EditTeamViewModel(Navigation, team);
            BindingContext = viewModel;
            NameEntry.Completed += (sender, e) => { viewModel.EditTeamCommand.Execute(null); };
        }
    }
}