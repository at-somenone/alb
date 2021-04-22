using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Archlab2.GuiClient
{
    public class UsersViewModel: BaseViewModel
    {
        private ChatModel model;

        public IReadOnlyList<string> Usernames {
            get => usernames;
            private set {
                usernames = value;
                OnPropertyChanged(nameof(Usernames));
            }
        }

        public string UsernameString {
            get => usernameString;
            set {
                usernameString = value;
                OnPropertyChanged(nameof(UsernameString));
                CmdRenameUser?.Invalidate();
            }
        }

        public UsersViewModel(ChatModel model) {
            this.model = model;
            model.UserListChanged += HandleUserListChanged;
            Usernames = new List<string>();
        }

        private void HandleUserListChanged(object sender, EventArgs e) {
            Usernames = new List<string>(model.usernames);
        }

        private RelayCommand cmdRenameUser;

        public RelayCommand CmdRenameUser {
            get {
                cmdRenameUser ??= new(_ => RenameUser(), _ => CanRenameUser());
                return cmdRenameUser;
            }
        }

        private bool CanRenameUser() {
            return UsernameString is { Length: > 0 and <= 32 }
                && !UsernameString.Any(char.IsWhiteSpace)
                && Usernames.All(u => u != UsernameString)
                && model.ConnectionState is ConnectionState.LoggedIn or ConnectionState.Connected;
        }

        private void RenameUser() {
            model.RenameUser(UsernameString);
        }


        private RelayCommand cmdRefreshUserList;
        private string usernameString;
        private IReadOnlyList<string> usernames;

        public ICommand CmdRefreshUserList {
            get {
                cmdRefreshUserList ??= new(_ => RefreshUserList(), _ => CanRefreshUserList());
                return cmdRefreshUserList;
            }
        }

        private bool CanRefreshUserList() {
            return model.ConnectionState is ConnectionState.LoggedIn or ConnectionState.Connected;
        }

        private void RefreshUserList() {
            model.RetrieveUsers();
        }
    }
}
