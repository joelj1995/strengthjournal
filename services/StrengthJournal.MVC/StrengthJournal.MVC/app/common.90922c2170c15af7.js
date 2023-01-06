"use strict";(self.webpackChunkstrengthjournal_app=self.webpackChunkstrengthjournal_app||[]).push([[592],{6281:(h,d,l)=>{l.d(d,{z:()=>r});var e=l(6580),_=l(5e3),s=l(520);let r=(()=>{class o extends e._{constructor(c){super(c)}getExercises(c,a,g){let m=`${this.BASE_URL}/exercises?pageNumber=${c}&perPage=${a}`;return""!=g&&(m+=`&search=${g}`),this.http.get(m)}getExercise(c){return this.http.get(`${this.BASE_URL}/exercises/${c}`)}getAllExercises(){return this.http.get(`${this.BASE_URL}/exercises?allRecords=true`)}getExerciseHistory(c,a,g,m=null){let f=`${this.BASE_URL}/exercises/${c}/history?pageNumber=${a}&perPage=${g}`;return m&&(f+=`&excludeWorkoutId=${m}`),this.http.get(f)}createExercise(c,a){return this.http.post(`${this.BASE_URL}/exercises`,{name:c,parentExerciseId:a})}updateExercise(c,a,g){return this.http.put(`${this.BASE_URL}/exercises/${c}`,{name:a,parentExerciseId:g})}deleteExercise(c){return this.http.delete(`${this.BASE_URL}/exercises/${c}`)}}return o.\u0275fac=function(c){return new(c||o)(_.LFG(s.eN))},o.\u0275prov=_.Yz7({token:o,factory:o.\u0275fac,providedIn:"root"}),o})()},2265:(h,d,l)=>{l.d(d,{h:()=>s});var e=l(5e3);const _=["*"];let s=(()=>{class r{constructor(){this.dismissDeleteEvent=new e.vpe,this.confirmDeleteEvent=new e.vpe}ngOnInit(){}}return r.\u0275fac=function(p){return new(p||r)},r.\u0275cmp=e.Xpm({type:r,selectors:[["app-confirm-delete"]],outputs:{dismissDeleteEvent:"dismissDeleteEvent",confirmDeleteEvent:"confirmDeleteEvent"},ngContentSelectors:_,decls:15,vars:0,consts:[["id","page-mask"],["id","defaultModalDanger","tabindex","-1","aria-modal","true","role","dialog",1,"modal","fade","show",2,"display","block"],["role","document",1,"modal-dialog","modal-sm"],[1,"modal-content"],[1,"modal-header"],[1,"modal-title"],["type","button","aria-label","Close",1,"btn-close",3,"click"],[1,"modal-body","m-3"],[1,"modal-footer"],["type","button",1,"btn","btn-secondary",3,"click"],["type","button",1,"btn","btn-danger",3,"click"]],template:function(p,c){1&p&&(e.F$t(),e._UZ(0,"div",0),e.TgZ(1,"div",1)(2,"div",2)(3,"div",3)(4,"div",4)(5,"h5",5),e._uU(6,"Confirm delete"),e.qZA(),e.TgZ(7,"button",6),e.NdJ("click",function(){return c.dismissDeleteEvent.emit()}),e.qZA()(),e.TgZ(8,"div",7),e.Hsn(9),e.qZA(),e.TgZ(10,"div",8)(11,"button",9),e.NdJ("click",function(){return c.dismissDeleteEvent.emit()}),e._uU(12,"Cancel"),e.qZA(),e.TgZ(13,"button",10),e.NdJ("click",function(){return c.confirmDeleteEvent.emit()}),e._uU(14,"Delete"),e.qZA()()()()())},styles:[""]}),r})()},9873:(h,d,l)=>{l.d(d,{_:()=>y});var e=l(5e3),_=l(6281),s=l(9808),r=l(8028);function o(i,u){if(1&i&&(e.TgZ(0,"td"),e._uU(1),e.qZA()),2&i){const t=e.oxw().$implicit;let n;e.xp6(1),e.hij("",null!==(n=null==t.bodyWeightKg?null:t.bodyWeightKg.toFixed(2))&&void 0!==n?n:"N/A"," kg ")}}function p(i,u){if(1&i&&(e.TgZ(0,"td"),e._uU(1),e.qZA()),2&i){const t=e.oxw().$implicit;let n;e.xp6(1),e.hij("",null!==(n=null==t.bodyWeightLbs?null:t.bodyWeightLbs.toFixed(2))&&void 0!==n?n:"N/A"," lbs")}}function c(i,u){if(1&i&&(e.TgZ(0,"td"),e._uU(1),e.qZA()),2&i){const t=e.oxw().$implicit;let n;e.xp6(1),e.hij("",null!==(n=null==t.weightKg?null:t.weightKg.toFixed(2))&&void 0!==n?n:"N/A"," kg ")}}function a(i,u){if(1&i&&(e.TgZ(0,"td"),e._uU(1),e.qZA()),2&i){const t=e.oxw().$implicit;let n;e.xp6(1),e.hij("",null!==(n=null==t.weightLbs?null:t.weightLbs.toFixed(2))&&void 0!==n?n:"N/A"," lbs")}}function g(i,u){if(1&i&&(e.TgZ(0,"tr")(1,"td"),e._uU(2),e.ALo(3,"date"),e.qZA(),e.YNc(4,o,2,1,"td",8),e.YNc(5,p,2,1,"td",8),e.YNc(6,c,2,1,"td",8),e.YNc(7,a,2,1,"td",8),e.TgZ(8,"td"),e._uU(9),e.qZA(),e.TgZ(10,"td"),e._uU(11),e.ALo(12,"rpe"),e.qZA()()),2&i){const t=u.$implicit,n=e.oxw(3);e.xp6(2),e.Oqu(e.xi3(3,7,t.entryDateUTC,"short")),e.xp6(2),e.Q6J("ngIf",n.displayKg),e.xp6(1),e.Q6J("ngIf",!n.displayKg),e.xp6(1),e.Q6J("ngIf",n.displayKg),e.xp6(1),e.Q6J("ngIf",!n.displayKg),e.xp6(2),e.Oqu(t.reps),e.xp6(2),e.Oqu(e.lcZ(12,10,t.rpe))}}function m(i,u){if(1&i&&(e.TgZ(0,"table",6)(1,"thead")(2,"tr")(3,"th"),e._uU(4,"Date"),e.qZA(),e.TgZ(5,"th"),e._uU(6,"Body Weight"),e.qZA(),e.TgZ(7,"th"),e._uU(8,"Weight"),e.qZA(),e.TgZ(9,"th"),e._uU(10,"Reps"),e.qZA(),e.TgZ(11,"th"),e._uU(12,"RPE"),e.qZA()()(),e.TgZ(13,"tbody"),e.YNc(14,g,13,12,"tr",7),e.qZA()()),2&i){const t=e.oxw(2);e.xp6(14),e.Q6J("ngForOf",t.historyList)}}function f(i,u){1&i&&(e.TgZ(0,"div",9)(1,"div",10)(2,"span",11),e._uU(3,"Loading..."),e.qZA()()())}function E(i,u){if(1&i){const t=e.EpF();e.TgZ(0,"div"),e.YNc(1,m,15,1,"table",2),e.TgZ(2,"div",3)(3,"button",4),e.NdJ("click",function(){return e.CHM(t),e.oxw().loadMore()}),e._uU(4,"Load More"),e.qZA()(),e.YNc(5,f,4,0,"ng-template",null,5,e.W1O),e.qZA()}if(2&i){const t=e.MAs(6),n=e.oxw();e.xp6(1),e.Q6J("ngIf",!n.loading)("ngIfElse",t),e.xp6(2),e.ekj("disabled",n.loadingMore||n.noMoreRecords)}}function b(i,u){1&i&&(e.TgZ(0,"p"),e._uU(1,"Choose an exercise from the picker to see its recent history."),e.qZA())}let y=(()=>{class i{constructor(t){this.exercises=t,this.historyList=[],this.loading=!0,this.loadingMore=!1,this.noMoreRecords=!1,this.pageNumber=1,this.perPage=10,this.exerciseId=null,this.excludeWorkoutId=null,this.displayKg=!0}ngOnInit(){this.loadExerciseHistory()}ngOnChanges(t){t.exerciseId&&this.loadExerciseHistory()}loadExerciseHistory(){!this.exerciseId||(this.noMoreRecords=!1,this.loadingMore=!1,this.pageNumber=1,this.loading=!0,this.exercises.getExerciseHistory(this.exerciseId,this.pageNumber,this.perPage,this.excludeWorkoutId).subscribe(t=>{this.loading=!1,t.totalRecords<=this.pageNumber*this.perPage&&(this.noMoreRecords=!0),this.historyList=t.data}))}loadMore(){!this.exerciseId||(this.loadingMore=!0,this.pageNumber+=1,this.exercises.getExerciseHistory(this.exerciseId,this.pageNumber,this.perPage,this.excludeWorkoutId).subscribe(t=>{this.loadingMore=!1,t.totalRecords<=this.pageNumber*this.perPage&&(this.noMoreRecords=!0),this.historyList=this.historyList.concat(t.data)}))}}return i.\u0275fac=function(t){return new(t||i)(e.Y36(_.z))},i.\u0275cmp=e.Xpm({type:i,selectors:[["app-exercise-history"]],inputs:{exerciseId:"exerciseId",excludeWorkoutId:"excludeWorkoutId",displayKg:"displayKg"},features:[e.TTD],decls:3,vars:2,consts:[[4,"ngIf","ngIfElse"],["noExerciseSelected",""],["class","table no-footer",4,"ngIf","ngIfElse"],[2,"display","flex","justify-content","center"],[1,"btn","btn-primary","btn-sm","mb-3",3,"click"],["loadingHistory",""],[1,"table","no-footer"],[4,"ngFor","ngForOf"],[4,"ngIf"],[1,"mb-2","text-center"],["role","status",1,"spinner-grow","me-2"],[1,"sr-only"]],template:function(t,n){if(1&t&&(e.YNc(0,E,7,4,"div",0),e.YNc(1,b,2,0,"ng-template",null,1,e.W1O)),2&t){const x=e.MAs(2);e.Q6J("ngIf",n.exerciseId)("ngIfElse",x)}},directives:[s.O5,s.sg],pipes:[s.uU,r.C],styles:[""]}),i})()},8028:(h,d,l)=>{l.d(d,{C:()=>_});var e=l(5e3);let _=(()=>{class s{transform(o){return o?(o/2).toString():""}}return s.\u0275fac=function(o){return new(o||s)},s.\u0275pipe=e.Yjl({name:"rpe",type:s,pure:!0}),s})()},4110:(h,d,l)=>{l.d(d,{K:()=>_});var e=l(5e3);let _=(()=>{class s{constructor(){this.editClicked=new e.vpe,this.deleteClicked=new e.vpe}ngOnInit(){}}return s.\u0275fac=function(o){return new(o||s)},s.\u0275cmp=e.Xpm({type:s,selectors:[["app-table-actions"]],outputs:{editClicked:"editClicked",deleteClicked:"deleteClicked"},decls:5,vars:0,consts:[[1,"svg-container",2,"text-align","center"],[3,"click"],["xmlns","http://www.w3.org/2000/svg","width","24","height","24","viewBox","0 0 24 24","fill","none","stroke","currentColor","stroke-width","2","stroke-linecap","round","stroke-linejoin","round",1,"feather","feather-trash","align-middle"],["points","3 6 5 6 21 6"],["d","M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"]],template:function(o,p){1&o&&(e.TgZ(0,"div",0)(1,"a",1),e.NdJ("click",function(){return p.deleteClicked.emit()}),e.O4$(),e.TgZ(2,"svg",2),e._UZ(3,"polyline",3)(4,"path",4),e.qZA()()())},styles:[""]}),s})()},8745:(h,d,l)=>{l.d(d,{Y:()=>_});var _=function(){function s(){this._subs=[]}return s.prototype.add=function(){for(var r=[],o=0;o<arguments.length;o++)r[o]=arguments[o];this._subs=this._subs.concat(r)},Object.defineProperty(s.prototype,"sink",{set:function(r){this._subs.push(r)},enumerable:!0,configurable:!0}),s.prototype.unsubscribe=function(){this._subs.forEach(function(r){return r&&function(s){return"function"==typeof s}(r.unsubscribe)&&r.unsubscribe()}),this._subs=[]},s}()}}]);