#!/bin/sh

sshd_config_needs_fixing() {
    if grep '^PermitRootLogin no$' /etc/ssh/sshd_config >/dev/null; then
        return 1
    fi
}

sudoers_needs_fixing() {
    if grep '^%sudo	ALL=(ALL:ALL) NOPASSWD:ALL$' /etc/sudoers >/dev/null; then
        return 1
    fi
}

if sudoers_needs_fixing; then
    echo "\nPatching sudoers for NOPASSWD access in 5 seconds... press ctrl+c to abort...\n"
    sleep 6
    cp /etc/sudoers /etc/sudoers.`date +"%Y-%d-%m"`.backup
    sed -i 's/^%sudo.*$/%sudo	ALL=(ALL:ALL) NOPASSWD:ALL/g' /etc/sudoers
fi

if sshd_config_needs_fixing; then
    echo "\nPatching sshd_config to disable root logins in 5 seconds... press ctrl+c to abort...\n"
    sleep 6
    cp /etc/ssh/sshd_config /etc/ssh/sshd_config.`date +"%Y-%d-%m"`.backup
    sed -i 's/^PermitRootLogin .*$/PermitRootLogin no/g' /etc/ssh/sshd_config
    systemctl restart ssh
fi
