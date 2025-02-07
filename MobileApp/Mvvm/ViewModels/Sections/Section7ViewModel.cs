using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections;

public partial class Section7ViewModel : SectionViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Diagnosis> _diagnoses = [
        new Diagnosis { Name = "F80.2 - Mixed receptive-expressive language disorder", IsChecked = false },
        new Diagnosis { Name = "F81.9 - Developmental disorder of scholastic skills, unspecified", IsChecked = false },
        new Diagnosis { Name = "F82 - Dyspraxia (Specific developmental disorder of motor function)", IsChecked = false },
        new Diagnosis { Name = "F84.0 - Autistic disorder", IsChecked = false },
        new Diagnosis { Name = "F88 - Other disorders of psychological development", IsChecked = false },
        new Diagnosis { Name = "F89 - Unspecified disorder of psychological development", IsChecked = false },
        new Diagnosis { Name = "G40.822 - Epileptic spasms, not intractable, without status epilepticus", IsChecked = false },
        new Diagnosis { Name = "G80.8 - Other cerebral palsy", IsChecked = false },
        new Diagnosis { Name = "G80.9 - Cerebral palsy, unspecified", IsChecked = false },
        new Diagnosis { Name = "G91.9 - Hydrocephalus, unspecified", IsChecked = false },
        new Diagnosis { Name = "H54.7 - Unspecified visual loss", IsChecked = false },
        new Diagnosis { Name = "H54.8 - Legal blindness, as defined in USA", IsChecked = false },
        new Diagnosis { Name = "H90.3 - Sensorineural hearing loss, bilateral", IsChecked = false },
        new Diagnosis { Name = "M62.81 - Muscle weakness (generalized)", IsChecked = false },
        new Diagnosis { Name = "P07.03 - Extremely low birth weight newborn, 750-999 grams", IsChecked = false },
        new Diagnosis { Name = "Q02 - Microcephaly", IsChecked = false },
        new Diagnosis { Name = "Q90.9 - Down syndrome, unspecified", IsChecked = false },
        new Diagnosis { Name = "R27.9 - Unspecified lack of coordination", IsChecked = false },
        new Diagnosis { Name = "R62.0 - Delayed milestone in childhood", IsChecked = false },
        new Diagnosis { Name = "R62.5 - Other and unspecified lack of expected normal physiological development in childhood", IsChecked = false },
        new Diagnosis { Name = "R62.50 - Unspecified lack of expected normal physiological development in childhood", IsChecked = false },
        new Diagnosis { Name = "R63.30 - Feeding difficulties, unspecified", IsChecked = false }
     ];

    public Section7ViewModel() : base("Diagnosis")
    {

    }
}

public partial class Diagnosis : ObservableObject
{
    [ObservableProperty]
    string name;
    [ObservableProperty]
    bool isChecked;
}
