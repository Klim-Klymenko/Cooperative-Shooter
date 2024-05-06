using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UI.View;
using UnityEngine;

namespace UI.Controller
{
    [UsedImplicitly]
    internal sealed class CurrentWeaponAdapter : IStartGameListener, IFinishGameListener
    {
        private IAtomicVariableObservable<AtomicObject> _currentWeapon;

        private IAtomicObject _currentObject;
        private IAtomicObject _previousObject;
        
        private readonly CurrentWeaponView _currentWeaponView;
        private readonly IAtomicObject _character;

        internal CurrentWeaponAdapter(CurrentWeaponView currentWeaponView, IAtomicObject character)
        {
            _currentWeaponView = currentWeaponView;
            _character = character;
        }

        void IStartGameListener.OnStart()
        {
            _currentWeapon = _character.GetVariableObservable<AtomicObject>(WeaponAPI.CurrentWeapon);
            _currentObject = _currentWeapon.Value;
            
            _currentWeapon.Subscribe(ChangeWeaponStats);
            ChangeWeaponStats(_currentWeapon.Value);
        }

        void IFinishGameListener.OnFinish()
        {
            _currentWeapon.Unsubscribe(ChangeWeaponStats);
        }

        private void ChangeWeaponStats(IAtomicObject currentWeapon)
        {
            Sprite weaponSprite = currentWeapon.Get<Sprite>(WeaponAPI.WeaponSprite);
            _currentWeaponView.ChangeImage(weaponSprite);
            
            _previousObject = _currentObject;
            _currentObject = currentWeapon;
            
            if (TryGetCharges(_previousObject, out IAtomicValueObservable<int> previousCharges))
                previousCharges.Unsubscribe(ChangeCharges);
            
            if (TryGetCharges(currentWeapon, out IAtomicValueObservable<int> charges))
            {
                ChangeCharges(charges.Value);
                charges.Subscribe(ChangeCharges);
                return;
            }
                
            _currentWeaponView.ChangeChargesText(string.Empty);
        }

        private void ChangeCharges(int charges)
        {
            _currentWeaponView.ChangeChargesText(charges.ToString());
        }

        private bool TryGetCharges(IAtomicObject weapon, out IAtomicValueObservable<int> charges)
        {
            if (weapon.TryGetValueObservable(WeaponAPI.Charges, out IAtomicValueObservable<int> result))
            {
                charges = result;
                return true;
            }
            
            charges = null;
            return false;
        }
    }
}