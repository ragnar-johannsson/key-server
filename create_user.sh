#!/bin/sh

USER="{{ user }}"
DEPENDENCIES="sudo useradd"

for DEPENDENCY in $DEPENDENCIES; do
    if ! which $DEPENDENCY >/dev/null 2>&1; then
        echo "Error: $DEPENDENCY not found. Exiting..."
        exit 1
    fi
done

echo "Creating user: $USER..."

if ! id $USER >/dev/null 2>&1; then

useradd -m -s /bin/bash -U -G sudo $USER
test -d /home/$USER/.ssh || mkdir /home/$USER/.ssh
cat <<EOF > /home/$USER/.ssh/authorized_keys
{{ keys }}
EOF
chown -R $USER:$USER /home/$USER/.ssh

else
    echo "User $USER already exists. Skipping..."
fi
