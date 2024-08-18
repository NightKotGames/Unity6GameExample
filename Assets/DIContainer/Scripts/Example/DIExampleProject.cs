using System;
using UnityEngine;

namespace DI
{
    // Сервис уровня Проекта
    internal class MyAwesomeProjectService { }

    // Сервис уровня Сцены
    internal class MySceneProjectService 
    {
        private readonly MyAwesomeProjectService _projectService;   
        internal MySceneProjectService(MyAwesomeProjectService myAwesomeProjectService) => _projectService = myAwesomeProjectService;
    }

    // Сервис уровня Фабрики
    internal class MyAwesomeFactory
    {
        // например что то с какими то параметрами
        internal MyAwesomeObject CreateInstance(string id, int par1) => new MyAwesomeObject(id, par1);
    }

    // Собственно Произодимый Фабрикой Класс
    internal class MyAwesomeObject
    {
        private readonly string _id;
        private readonly int par1;

        internal MyAwesomeObject(string id, int par1)
        {
            _id = id;
            this.par1 = par1;   
        }
    }



    internal class DIExampleProject : MonoBehaviour
    {
        // Делаем Регистрацию
        private void Start()
        {
            var projectContainer = new DIContainer();
            // Простые (или Глобальные Конейнеры делаем как Синглтон)
            projectContainer.RegisterSingleton(_ => new MyAwesomeProjectService()); // Нижнее подчеркивание если ссылка не нужна
            projectContainer.RegisterSingleton("option1", _ => new MyAwesomeProjectService());
            projectContainer.RegisterSingleton("option2", _ => new MyAwesomeProjectService());

            // Какое то переключение Сцен

            // Запуск после Загрузки Сцены
            var sceneRoot = FindObjectOfType<DIExampleScene>();
            sceneRoot.Init(projectContainer);
        }
    }
}