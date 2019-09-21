var el=document.getElementById("bars");
el.addEventListener("click",(function(){
    var el1=document.getElementById("bar1");
    var el2=document.getElementById("bar2");
    var el3=document.getElementById("bar3");
    el1.classList.toggle("class1");
    el2.classList.toggle("class3");
   el3.classList.toggle("class2");
}));