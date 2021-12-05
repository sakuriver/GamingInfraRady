using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace BaseCampResponseData
{


    [Serializable]
    public class CloudServerResponse
    {
        [SerializeField]
        public int From;
        [SerializeField]
        public int Count;
        [SerializeField]
        public int Total;
        [SerializeField]
        public CloudServerInfo[] Servers;
    }

    [Serializable]
    public class CloudServerInfo
    {
        [SerializeField]
        public String ID;
        [SerializeField]
        public String NAME;

    }


    [Serializable]
    public class VersionControlSystemGitRoot
    {
        [SerializeField]
        public string login;
        [SerializeField]
        public int id;
        [SerializeField]
        public string node_id;
        [SerializeField]
        public string avatar_url;
        [SerializeField]
        public string gravatar_id;
        [SerializeField]
        public string url;
        [SerializeField]
        public string html_url;
        [SerializeField]
        public string followers_url;
        [SerializeField]
        public string following_url;
        [SerializeField]
        public string gists_url;
        [SerializeField]
        public string starred_url;
        [SerializeField]
        public string subscriptions_url;
        [SerializeField]
        public string organizations_url;
        [SerializeField]
        public string repos_url;
        [SerializeField]
        public string events_url;
        [SerializeField]
        public string received_events_url;
        [SerializeField]
        public string type;
        [SerializeField]
        public bool site_admin;
        [SerializeField]
        public string name;
        [SerializeField]
        public string company;
        [SerializeField]
        public string blog;
        [SerializeField]
        public string location;
        [SerializeField]
        public object email;
        [SerializeField]
        public object hireable;
        [SerializeField]
        public object bio;
        [SerializeField]
        public object twitter_username;
        [SerializeField]
        public int public_repos;
        [SerializeField]
        public int public_gists;
        [SerializeField]
        public int followers;
        [SerializeField]
        public int following;
        [SerializeField]
        public DateTime created_at;
        [SerializeField]
        public DateTime updated_at;
    }

}
