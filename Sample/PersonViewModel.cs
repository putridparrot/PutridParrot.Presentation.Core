﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using PutridParrot.Presentation;

namespace Sample
{
    //[MetadataType(typeof(PersonViewModel))]
    public class PersonViewModel : ViewModel
    {
        public PersonViewModel()
        {
            ValidateCommand = new ActionCommand(() => Validate());
            AcceptCommand = new ActionCommand(() => ((IRevertibleChangeTracking)this).AcceptChanges());
            RevertCommand = new ActionCommand(() => ((IRevertibleChangeTracking)this).RejectChanges());
        }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        [Required]
        [Range(16, Int32.MaxValue, ErrorMessage = "Person must be older than 16")]
        [DefaultValue(16)]
        public int Age
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public string FullName => ReadOnlyProperty(() => $"{FirstName} {LastName}");

        public ICommand ValidateCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand RevertCommand { get; set; }
    }
}
