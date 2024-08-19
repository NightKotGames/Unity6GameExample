using UnityEngine;

namespace DI
{
    internal class DIExampleScene : MonoBehaviour
    {
        internal void Init(DIContainer projectContainer)
        {
            // используем уровень проекта, чтобы вытащить то, что нам нужно

            //var serviceWitchoutTag = projectContainer.Resolve<MyAwesomeProjectService>();
            //var service1 = projectContainer.Resolve<MyAwesomeProjectService>("option1");
            //var service2 = projectContainer.Resolve<MyAwesomeProjectService>("option2");

            var sceneContainer = new DIContainer(projectContainer);
            sceneContainer.RegisterSingleton(c => new MySceneProjectService(c.Resolve<MyAwesomeProjectService>()));
            sceneContainer.RegisterSingleton(_ => new MyAwesomeFactory()); // Регистрируем Фабрику
            sceneContainer.RegisterInstance(new MyAwesomeObject("instanceID", 100)); // Регистрируем Продукт Фабрики

            var objectFactory = sceneContainer.Resolve<MyAwesomeFactory>(); // Достаем из Конейнера Фабрику

            for (int i = 0; i < 3; i++)
            {
                var id = $"o{i}";
                var o = objectFactory.CreateInstance(id,i);
                Debug.Log($"Object Created in Factory \n{o}");
            }

            var instance = sceneContainer.Resolve<MyAwesomeObject>();
            Debug.Log($"\n Object - {instance} - Instance");
        }
    }
}