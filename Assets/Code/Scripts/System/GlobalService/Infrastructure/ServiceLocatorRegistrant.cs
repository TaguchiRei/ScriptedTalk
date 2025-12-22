using UnityEngine;

namespace ScriptedTalk
{
    /// <summary>
    /// 指定されたコンポーネントを走査し、RegisterableService属性を持つインターフェースを
    /// ServiceLocatorに自動的に登録します。
    /// </summary>
    public class ServiceLocatorRegistrant : MonoBehaviour
    {
        [SerializeField]
        private Component[] _servicesToRegister;

        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            if (ServiceLocator.Instance == null)
            {
                Debug.LogError("ServiceLocator instance not found. Make sure a ServiceLocator is active in the scene.");
                return;
            }

            var serviceLocatorType = typeof(ServiceLocator);
            var tryRegisterMethodInfo = serviceLocatorType.GetMethod("TryRegister");

            if (tryRegisterMethodInfo == null)
            {
                Debug.LogError("TryRegister method not found in ServiceLocator.");
                return;
            }

            foreach (var serviceComponent in _servicesToRegister)
            {
                if (serviceComponent == null)
                {
                    Debug.LogWarning("A null component is assigned in the ServiceLocatorRegistrant.", this);
                    continue;
                }

                var componentType = serviceComponent.GetType();
                var interfaces = componentType.GetInterfaces();

                var registeredAny = false;

                foreach (var interfaceType in interfaces)
                {
                    if (System.Attribute.IsDefined(interfaceType, typeof(RegisterableServiceAttribute)))
                    {
                        // ジェネリックメソッドを作成
                        var genericMethod = tryRegisterMethodInfo.MakeGenericMethod(interfaceType);

                        // メソッドを呼び出し
                        var result = (bool)genericMethod.Invoke(ServiceLocator.Instance, new object[] { serviceComponent });

                        if (result)
                        {
                            Debug.Log($"Successfully registered {componentType.Name} as {interfaceType.Name}");
                            registeredAny = true;
                        }
                        else
                        {
                            Debug.LogWarning($"Failed to register {componentType.Name} as {interfaceType.Name}. It might be already registered.");
                        }
                    }
                }

                if (!registeredAny)
                {
                    Debug.LogWarning($"No registerable interfaces with [RegisterableService] attribute found on {componentType.Name}.", serviceComponent);
                }
            }
        }
    }
}
