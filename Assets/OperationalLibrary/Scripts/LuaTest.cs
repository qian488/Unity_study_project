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
            Debug.Log("����csharp����");
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
                    print('����lua�ķ���')
                end)
            ");
            t.Say();
        }

        void Update()
        {

        }
    }

}
