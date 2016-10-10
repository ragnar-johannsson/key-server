## Key Server

An API server delivering SSH public keys for users and groups, allowing the creation of users through a simple `curl keyserver/<user or group> | sh`. The structure of user groups can be as nested or as flat as required.

### Setup & usage

Run the docker container `ragnarb/key-server` with a directory volume mounted into */opt/keys* to hold the user keys. See [cloud-config.yml](cloud-config.yml) for an example on how to run the key server on CoreOS with [Caddy](https://caddyserver.com/) providing automatic HTTPS.

The format of users and keys is straight forward: Simply create directories for every user and populate with an *authorized_keys* file containing the public keys for that user. Groups are similar: Simply create directories for them and symlink individual user or group directories in there.

A sample key dir tree should thus look something like:

```
├── alice
│   └── authorized_keys
├── bob
│   └── authorized_keys
├── group1
│   ├── alice -> ../alice
│   └── bob -> ../bob
└── group2
    └── alice -> ../alice
```

Calling a `curl keyserver/group1 | sh` in this case would create users for both Alice and Bob using the keys in the *authorized_keys* file under their respective directories.

### Further details

See [Config.fs](Config.fs) for details on environment variables exposed for further fine-tuning, such as providing your own user creation script template.

### Disclaimer

Blindly piping the output of curl into a shell is always dubious. Make sure you trust the server running the key server as well as the transport, and that the directory containing the keys and location of the user creation scripts are secure before engaging with the server. The usual warnings and disclaimers apply.

### License

BSD 2-Clause. See the LICENSE file for details.
