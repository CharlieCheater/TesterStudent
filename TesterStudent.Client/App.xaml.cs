using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TesterStudent.Client.Domain;

namespace TesterStudent.Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static readonly ApiClient Client = new ApiClient("https://localhost:7145/");
}