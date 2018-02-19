using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloniaProofOfConcept
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
