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
            sceneContainer.RegisterSingleton(_ => new MyAwesomeFactory());
            sceneContainer.RegisterInstance(new MyAwesomeObject("instanceID", 100));

        }
    }
}