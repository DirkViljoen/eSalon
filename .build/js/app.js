'use strict';
function createpicker(vID){
  var index;
  var a = document.getElementByID(vID);
  document.write(vID);
  var pickers = [];
  for (index = 0; index < a.length; ++index) {
    pickers[pickers.length] = new Pikaday({ field:  a[index]});
    console.log(a[index]);
  };
}



