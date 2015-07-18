'use strict';


module.exports = function RoleModel() {
    return {
        name: 'role',
        roleName: '1',
        roleNames:
          [
            {
              'id': '',
              'value': 'choose a role'
            },
            {
              'id': '1',
              'value': 'Manager'
            },
            {
              'id': '2',
              'value': 'stylist'
            }
          ]
    };
};
