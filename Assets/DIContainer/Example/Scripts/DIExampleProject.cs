
using UnityEngine;

namespace DI
{
    // ������ ������ �������
    internal class MyAwesomeProjectService { }

    // ������ ������ �����
    internal class MySceneProjectService 
    {
        private readonly MyAwesomeProjectService _projectService;   
        internal MySceneProjectService(MyAwesomeProjectService myAwesomeProjectService) => _projectService = myAwesomeProjectService;
    }

    // ������ ������ �������
    internal class MyAwesomeFactory
    {
        // �������� ��� �� � ������ �� �����������
        internal MyAwesomeObject CreateInstance(string id, int par1) => new MyAwesomeObject(id, par1);
    }

    // ���������� ����������� �������� �����
    internal class MyAwesomeObject
    {
        private readonly string _id;
        private readonly int _par1;

        internal MyAwesomeObject(string id, int par1)
        {
            _id = id;
            this._par1 = par1;   
        }

        public override string ToString() => $"Object with ID: {_id} and part1: {_par1}";
    }



    internal class DIExampleProject : MonoBehaviour
    {
        // ������ �����������
        private void Start()
        {
            var projectContainer = new DIContainer();
            // ������� (��� ���������� ��������� ������ ��� ��������)
            projectContainer.RegisterSingleton(_ => new MyAwesomeProjectService()); // ������ ������������� ���� ������ �� �����
            projectContainer.RegisterSingleton("option1", _ => new MyAwesomeProjectService());
            projectContainer.RegisterSingleton("option2", _ => new MyAwesomeProjectService());

            // ����� �� ������������ ����

            // ������ ����� �������� �����
            var sceneRoot = FindObjectOfType<DIExampleScene>();
            sceneRoot.Init(projectContainer);
        }
    }
}