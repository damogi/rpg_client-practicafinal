using System;
using System.Collections;
using System.Collections.Generic;
using CharacterSelection;
using UnityEngine;
using UnityRest;
using Characters;
using Players;

namespace Services
{
    public class ServicesFacade: MonoBehaviour
    {
        public CharacterCatalog catalog;

        public static ServicesFacade Instance
        {
            get; private set;
        }

        public string userId = "0";

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(gameObject);
        }

        private void OnApplicationQuit()
        {
            foreach (CharacterData character in catalog.characters)
            {
                character.isAvailable = false;
            }
        }

        public void Login(string userName, Action onSuccess)
        {
            // TODO: Call login service using username, if successful invoke callback and set userId
            UnityRestManager.Instance.Get<UserLogin>().OnResult<UserLogin>(LogUser).WithId(userName).Send();

            StartCoroutine(CheckUser(userName, onSuccess));
        }

        private void LogUser(UserLogin user)
        {
            Debug.Log(user.Print());

            UserLogin.actualUser = user;
        }

        private void LogUsers(UserLogin[] users)
        {
            foreach (UserLogin user in users)
            {
                LogUser(user);
            }
        }

        public void FetchTeam(Action<Team> onSuccess)
        {
            // TODO: Call team service using the userId to retrieve the current characters in his team
            // if none is available create a new one empty.
            UnityRestManager.Instance.Get<Team>().OnResult<Team>(LogTeam).WithId(userId).Send();

            StartCoroutine(CheckTeam(onSuccess));
        }

        public void SaveTeam(Action onSuccess)
        {
            string body = "";

            if (Team.actualTeam != null)
                body = string.Format("character1 : {0}, character2: {1}, character3: {2}",
                    Team.actualTeam.character1,
                    Team.actualTeam.character2,
                    Team.actualTeam.character3);
            else
                Debug.LogWarning("No body to send");

            if (Team.actualTeam != null)
                UnityRestManager.Instance.Post<Team>().WithBody(body).OnResult(SetTeam).WithId(userId).Send();

            onSuccess();
        }

        private void LogTeam(Team team)
        {
            Debug.Log(team.Print());

            Team.actualTeam = team;
        }

        private void LogTeams(Team[] teams)
        {
            foreach(Team team in teams)
            {
                LogTeam(team);
            }
        }

        private void SetTeam()
        {
            Debug.Log("POST finish!");
        }

        public void FetchGold(Action<int> onSuccess)
        {
            // TODO: Call get gold service
            UnityRestManager.Instance.Get<Gold>().OnResult<Gold>(LogGold).WithId(userId).Send();

            StartCoroutine(CheckGold(onSuccess));
        }

        private void LogGold(Gold gold)
        {
            Debug.Log(gold.Print());

            Gold.actualGold = gold;
        }

        public void GetAvailableCharacters(Action<List<ObjectId>> onSuccess)
        {
            UnityRestManager.Instance.Get<Character>().OnResult<Character>(LogCharacter).WithId(userId).Send();

            StartCoroutine(CheckCharacter(onSuccess));
        }

        public void PurchaseCharacter(CharacterData character, Action onSuccess)
        {
            onSuccess();

            string body = string.Format("gold: {0}, newCharacter: {1}, health: {2}, attack: {3}, defense: {4}",
                Player.Instance.Gold,
                character.id.value,
                character.lifepoints,
                character.attackpoints,
                character.defensepoints);

            UnityRestManager.Instance.Post<Character>().WithBody(body).OnResult(SetCharacter).WithId(userId).Send();
            // TODO: Call purchasing service.

        }

        private void LogCharacter(Character character)
        {
            Debug.Log(character.Print());

            Character.actualCharacter = character;
        }

        private void LogCharacters(Character[] characters)
        {
            foreach (Character character in characters)
            {
                LogCharacter(character);
            }
        }

        private void SetCharacter()
        {
            Debug.Log("POST finish!!");
        }

        private IEnumerator CheckUser(string userName, Action onSuccess)
        {
            while (UserLogin.actualUser == null)
            {
                yield return null;
            }

            if (UserLogin.actualUser.name != userName)
            {
                Debug.LogWarning("This user doesn't exist");
            }
            else
            {
                userId = UserLogin.actualUser.userId.ToString();
                onSuccess();
            }
        }

        private IEnumerator CheckTeam(Action<Team> onSuccess)
        {
            while (Team.actualTeam == null)
            {
                yield return null;
            }

            onSuccess(Team.actualTeam);
        }

        private IEnumerator CheckGold(Action<int> onSuccess)
        {
            while (Gold.actualGold == null)
            {
                yield return null;
            }

            onSuccess(Gold.actualGold.gold);
        }

        private IEnumerator CheckCharacter(Action<List<ObjectId>> onSuccess)
        {
            while (Character.actualCharacter == null)
            {
                yield return null;
            }

            onSuccess(Character.actualCharacter.availablesCharacters);
        }
    }
}