using GameCycle;
using UI.Factories;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.Managers
{
    internal sealed class BarManager : MonoBehaviour, IStartGameListener
    {
        //TODO: Add Armor UI Logic and assign array
        
        /*private Common.IFactory<BarView>[] _barViewFactories;
        private IBarAdapterFactory[] _barAdapterFactories;*/
        
        private Common.IFactory<BarView> _barViewFactory;
        private IBarAdapterFactory _barAdapterFactory;
        
        /*[Inject]
        internal void Construct(Common.IFactory<BarView>[] barViewFactories, IBarAdapterFactory[] barAdapterFactories)
        {
            _barViewFactories = barViewFactories;
            _barAdapterFactories = barAdapterFactories;
        }*/
        
        [Inject]
        internal void Construct(Common.IFactory<BarView> barViewFactory, IBarAdapterFactory barAdapterFactory)
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