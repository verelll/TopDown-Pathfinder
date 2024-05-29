using Cysharp.Threading.Tasks;
using Pathfinder.GameCharacter;
using Pathfinder.GameStates;
using Pathfinder.Locations;
using Pathfinder.Menu;
using Pathfinder.ScreenLoading;
using UnityEngine;
using verell.Architecture;
using verell.CameraHolder;
using verell.Saves;
using verell.StateMachine;
using verell.UI;
using verell.Inputs;

namespace Pathfinder.Main
{
	public sealed class GameEntryPoint : MonoBehaviour
	{
		private const string ContainerName = "MainContainer";

		private Container _container;
		private IStateMachine _gameStateMachine;

		private async void Start()
		{
			_container = new Container(ContainerName);

			RegisterSaves();
			RegisterServices();
			
			_container.ApplyDependencies();
			await Progress();
		}

		private async void OnApplicationQuit()
		{
			await _container.Dispose();
			var container = _container as IContainer;
			var savesService = container.Get<SavesService>();
			savesService.SaveAllAsync().Forget();
		}

		private async UniTask Progress()
		{
			_gameStateMachine = new GameStateMachine(_container);
			await _gameStateMachine.ChangeState<LoadingState>();
			await _container.Init();
			await _gameStateMachine.ChangeState<MenuState>();
		}

		private void RegisterSaves()
		{
			_container.Add<CharacterSaveData>();
		}
		
		private void RegisterServices()
		{
			_container.Add<SavesService>();
			_container.Add<LoadingScreenService>();
			_container.Add<UIService>();
			_container.Add<InputService>();
			_container.Add<LocationsService>();
			_container.Add<LocationsInteractionService>();
			_container.Add<MenuService>();
			_container.Add<CameraService>();
			_container.Add<CharacterCreatorService>();
			_container.Add<CharacterControllableService>();
		}
	}
}
