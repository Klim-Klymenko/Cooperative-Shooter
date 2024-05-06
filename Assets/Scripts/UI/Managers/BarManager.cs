using GameCycle;
using JetBrains.Annotations;
using UI.Factories;
using UI.View;

namespace UI.Managers
{
    [UsedImplicitly]
    internal sealed class BarManager : IStartGameListener
    {
        //TODO: Add Armor UI Logic and assign array
        
        /*private readonly Common.IFactory<BarView>[] _barViewFactories;
        private readonly IBarAdapterFactory[] _barAdapterFactories;*/
        
        private readonly Common.IFactory<BarView> _barViewFactory;
        private readonly IBarAdapterFactory _barAdapterFactory;
        
        /*internal BarManager(Common.IFactory<BarView>[] barViewFactories, IBarAdapterFactory[] barAdapterFactories)
        {
            _barViewFactories = barViewFactories;
            _barAdapterFactories = barAdapterFactories;
        }*/
        
        internal BarManager(Common.IFactory<BarView> barViewFactory, IBarAdapterFactory barAdapterFactory)
        {
            _barViewFactory = barViewFactory;
            _barAdapterFactory = barAdapterFactory;
        }
        
        void IStartGameListener.OnStart()
        {
            /*for (int i = 0; i < _barViewFactories.Length; i++)
            {
                BarView barView = _barViewFactories[i].Create();
                _barAdapterFactories[i].Create(barView);
            }*/
            
            BarView healthView = _barViewFactory.Create();
            _barAdapterFactory.Create(healthView);
        }
    }
}