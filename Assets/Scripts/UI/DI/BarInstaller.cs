﻿using Atomic.Objects;
using UI.Controller;
using UI.Factories;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.DI
{
    internal sealed class BarInstaller : MonoInstaller
    {
        [SerializeField] 
        private BarView[] _barViewPrefabs;
        
        [SerializeField]
        private Transform _barViewContainer;
        
        [SerializeField]
        private AtomicObject _character;
        
        public override void InstallBindings()
        {
            BindBarViewFactories();
            BindBarAdapterFactories();
        }
        
        private void BindBarViewFactories()
        {
            for (int i = 0; i < _barViewPrefabs.Length; i++)
                Container.Bind<Common.IFactory<BarView>>().To<BarViewFactory>().AsCached().WithArguments(_barViewPrefabs[i], _barViewContainer);
        }
        
        private void BindBarAdapterFactories()
        {
            Container.Bind<IBarAdapterFactory>().To<BarAdapterFactory<HealthAdapter>>().AsCached().WithArguments(_character);
            //TODO: Add ArmorAdapter
        }
    }
}