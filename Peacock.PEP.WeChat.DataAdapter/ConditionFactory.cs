﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.PEP.WeChat.Service;
using Peacock.PEP.WeChat.DataAdapter.Interface;
using Peacock.PEP.WeChat.DataAdapter.Implement;

namespace Peacock.PEP.WeChat.DataAdapter
{
    /// <summary>
    /// 指定接口的实现类型
    /// </summary>
    public class ConditionFactory
    {
        private static readonly ConditionFactory _Instance = new ConditionFactory();
        private Dictionary<Type, object> _items;
        private ConditionFactory()
        {
            _items = new Dictionary<Type, object>();
            Registered<IUserAdapter, UserAdapter>(new UserAdapter());
            Registered<IChargeAdapter, ChargeAdapter>(new ChargeAdapter());
            Registered<IAuthCheckAdapter, AuthCheckAdapter>(new AuthCheckAdapter());
        } 

        public static ConditionFactory Conditions
        {
            get
            {
                return _Instance;
            }
        }

        private void Registered<TInterface, TImplement>(TImplement obj) where TImplement : TInterface
        {
            if (_items.ContainsKey(typeof(TInterface)))
                throw new Exception(string.Format("{0}类型已注册"));
            _items.Add(typeof(TInterface), obj);
        }

        public TResult Resolve<TResult>()
        {
            if (!typeof(TResult).IsInterface)
                throw new Exception(string.Format("{0}类型TResult必须是接口类型", typeof(TResult).Name));
            if (!_items.ContainsKey(typeof(TResult)))
                throw new Exception(string.Format("{0}类型尚未注册", typeof(TResult).Name));
            return (TResult)_items[typeof(TResult)];
        }

    }
}
