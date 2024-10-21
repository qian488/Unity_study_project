using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using XLua;

namespace Operation
{
    [Hotfix]
    public class LuaTT
    {
        public void Say()
        {
            Debug.Log("我是csharp方法");
        }
    }
    public class LuaTest : MonoBehaviour
    {
        void Start()
        {
            LuaTT t = new LuaTT();
            t.Say();
            LuaEnv luaEnv = new LuaEnv();
            luaEnv.DoString(@"
                xlua.hotfix(CS.Operation.LuaTT,'Say',function()
                    print('我是lua的方法')
                end)
            ");
            t.Say();
        }

        void Update()
        {

        }
    }

}
