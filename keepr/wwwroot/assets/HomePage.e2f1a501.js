import{_ as f,A as t,k as g,l as k,P as v}from"./index.c39ce0d3.js";import{M as l,c as p,a as m,o,b as s,i as _,d as n,F as i,l as y,e as x,f as w,p as C,m as I}from"./vendor.d2861f43.js";const K={name:"Home",beforeUnmount(){t.activeKeeps=null},watch:{openModal(e){e?l.getOrCreateInstance(document.getElementById("keep-modal")).show():l.getOrCreateInstance(document.getElementById("keep-modal")).hide()}},async mounted(){try{t.searchTerm="",t.activeKeeps=null,await g.getAll()}catch(e){k.error("[HomePage.vue > mounted]",e.message),v.toast(e.message,"error")}},setup(){return{keeps:p(()=>{var e;return(e=t.activeKeeps)==null?void 0:e.filter(r=>r.name.toLowerCase().includes(t.searchTerm.toLowerCase()))}),openModal:p(()=>t.openModal)}}},M=e=>(C("data-v-63bc395f"),e=e(),I(),e),B={key:0,class:"w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex"},b=M(()=>n("div",{class:"spinner-border text-secondary"},null,-1)),P=[b],S={class:"mx-3 mx-md-5"},H={key:0,class:"mt-5 text-center text-secondary"},j={class:"masonry-with-columns"};function A(e,r,E,a,L,N){var c;const u=m("KeepCard"),h=m("KeepModal");return o(),s(i,null,[a.keeps?_("",!0):(o(),s("div",B,P)),n("div",S,[((c=a.keeps)==null?void 0:c.length)==0?(o(),s("h1",H,"There are no keeps")):_("",!0),n("div",j,[(o(!0),s(i,null,y(a.keeps,d=>(o(),w(u,{key:d.id,keep:d},null,8,["keep"]))),128))])]),x(h,{id:"keep-modal"})],64)}var F=f(K,[["render",A],["__scopeId","data-v-63bc395f"]]);export{F as default};
