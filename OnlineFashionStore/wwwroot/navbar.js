var categories=document.getElementById('AllCategory')
var catList=document.getElementById('Categories')
catList.style.display="none";
categories.addEventListener("click", () => {
    if(catList.style.display=="none"){
        catList.style.display="flex";
        console.log('1')
    }else{
        catList.style.display="none";
        console.log('2')
    }
});
var leftCat=document.getElementsByClassName('left-side-container');
for (const item of leftCat) {
    item.addEventListener('click',()=>{

    });
    
}
// leftCat.forEach(element => {
// });