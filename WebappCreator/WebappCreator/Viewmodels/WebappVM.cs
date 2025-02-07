﻿using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows;
using WebappCreator.Helpers;
using System.Windows;
using WebappCreator.Models;
using System.Windows.Data;

namespace WebappCreator.Viewmodels
{
    public class WebappVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region properties
        public DelegateCommand CreateProject { get; set;  }
        public DelegateCommand OpenProject { get; set;  }

        public string OpenedPath { get; set; }

        /** Property to memorize the index.html file path in the project*/
        public string ProjectPath { get; set;  }

        #endregion

        #region variables

        List<TreeItem> treeItems = new List<TreeItem>()
            {
                new TreeItem
                {
                    Name = "TreeItem 1"
                },
                new TreeItem
                {
                    Name = "TreeItem 2"
                },
                new TreeItem
                {
                    Name = "TreeItem 3"
                },
                new TreeItem
                {
                    Name = "TreeItem 4"
                }
            };


        #endregion

        #region constructor
        public WebappVM() 
        {
            CreateProject = new DelegateCommand(OpenFolder);
            OpenProject = new DelegateCommand(OpenFile);
        }
        #endregion

        #region methods
        /// <summary>
        /// Metodo che permette di lanciare la dialog per chiedere la selezione di
        /// una cartella per contenente un progetto da aprire
        /// </summary>
        public void OpenFolder() 
        {
            string folder = FolderPicker.OpenFolderDialog(
                FileSystemHelper.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            bool success3 = FileHelper.CreateMultipleFiles(
                Models.FileInfo.GetFiles(
                        new string[][]
                        {   
                            new string[] { "Index.html", folder, "<div style=\"width:100vw;height:100vh;\">\n\t<iframe style=\"width:100vw; height:100vh;border-width: 0;\" src=\"home/home.html\"></iframe>\n</div>"},
                            new string[] { "Home.html", folder + "\\Home", "<html><header><!-- #region imports --><script type=\"text/javascript\" src=\"Home.js\"></script><link rel=\"stylesheet\" href=\"Home.css\"><!-- #endregion --></header><body>HELLO WORLD</body></html>"},
                            new string[] { "Home.css", folder + "\\Home", "body {\n  background: aqua; \n}"},
                            new string[] { "Home.js", folder + "\\Home", "console.log('Hello world');"},
                        }
                    ));

            // TODO: set actual created ptoject as opened, then use a tree to display it on screen
            this.OpenedPath = success3 ? folder : this.OpenedPath;
        }

        public void OpenFile()
        {
            string file = FilePicker.OpenFileDialog(
                FileSystemHelper.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Index.html",
                ".html",
                "Main project file (.html)|*.html");
            Console.WriteLine(file);
        }


        #endregion

        public ICollectionView Books
        {
            get
            {
                var source = CollectionViewSource.GetDefaultView(this.treeItems);
                source.GroupDescriptions.Add(new PropertyGroupDescription("Type")
            );
                return source;
            }
        }
    }
}
