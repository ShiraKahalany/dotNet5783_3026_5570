(self.__LOADABLE_LOADED_CHUNKS__=self.__LOADABLE_LOADED_CHUNKS__||[]).push([[12506,47974],{365804:(e,t,r)=>{r.d(t,{C:()=>s,Z:()=>_});var n=r(957161),o=r(773285),a=r(276775);const s="lastOnHomefeed",i=["/","/homefeed/"];function _(){const{pathname:e}=(0,a.useLocation)(),t=(0,a.useHistory)(),{checkExperiment:r}=(0,o.F)(),_=i.includes(e),c=t&&"POP"!==t.action;if(_&&c){const e=JSON.parse((0,n.qn)(s)),t=null!=e&&e.time?Date.now()-e.time:0,o=c&&t>0?r("hfp_hf_refresh_logic_web"):{};return{shouldMaintainHomefeed:"enabled_2_5_min"===o.group&&t/6e4<2.5||"enabled_1_min"===o.group&&t/6e4<1,lastHFScrollPosition:null==e?void 0:e.scrollPosition}}return{shouldMaintainHomefeed:!1}}},422722:(e,t,r)=>{r.r(t),r.d(t,{default:()=>_});var n=r(176532),o=r(635240),a=r(395164);const s={};s.BASE_URL_PATH="/_/_/report/",s.BASE_URL_PATH_TRACE="/_/_/trace/",s.SERVER_LOG_TYPE=Object.freeze({PAGE_LOAD_METRIC:"page_load_metric",BROWSER_EXTENSION:"browser_extension",RENDER_TIME:"render_time",NETWORK_LOAD:"network_load",SIGNUP_LOAD:"signup_load",TRACE:"trace",STRING_USAGE:"string_usage",HF_REFRESH:"hf_refresh"}),s.HTTP_METHOD=Object.freeze({GET:"GET",POST:"POST"});const i=function(e,t){if(!/password/i.test(e))return t};s._assembleXhrData=function(e,t){let r;try{r=JSON.stringify(t,i)}catch(n){r=JSON.stringify({messages:[n.toString()]})}return{report_context:JSON.stringify(e,i),report_data:r}},s.logToServer=function(e,t,r,i=0,_,c){var l;let u=null!=i?i:0;const f=null!=_?_:1;c=null!==(l=c)&&void 0!==l?l:_,function(e,t,r,a){let i;i="trace"===e?s.BASE_URL_PATH_TRACE+e+"/":s.BASE_URL_PATH+e+"/",a&&(r.timeElapsed=Date.now()-a),e&&(r.logType=e);let _="";try{_=window.location.href}catch(u){}const c={app_version:(0,o.Z)(),current_url:_},l=s._assembleXhrData(c,r);return(0,n.Z)({url:i,type:t,data:l}).promise.then((({ok:e,status:t,statusText:r})=>e?Promise.resolve():Promise.reject({type:"error",message:r,httpStatus:t})))}(e,t,r,c).catch((n=>{if(u){u-=1;const n=Math.min(18e5,1.23*f);setTimeout((()=>{s.logToServer(e,t,r,u,n,c)}),_)}else u-=1,(0,a.T)({name:n.name,message:`LogToServer failed: ${n.message}`})}))},s.logPerfDataToServer=s.logToServer,s.logToServer=function(e,t){let r=0;return function(...n){if(!(r>=t))return r+=1,e.apply(this,n)}}(s.logToServer,10);const _=s},327956:(e,t,r)=>{r.d(t,{Z:()=>s});var n=r(667294),o=r(407043),a=r(385740);const s=e=>{const t=(0,n.useRef)(!1),{logContextEvent:r}=(0,o.v)(),{viewType:s,viewParameter:i}=(0,a.SU)();(0,n.useEffect)((()=>{Object.entries(e).length>0&&!t.current&&s&&(r({event_type:13,view_type:s,view_parameter:i,...e}),t.current=!0)}))}},376113:(e,t,r)=>{r.d(t,{S:()=>a,i:()=>o});var n=r(773285);const o="web_today_tab_v1";function a(){return(0,n.F)().checkExperiment(o).anyEnabled}},224525:(e,t,r)=>{function n(e,t){return t?`${e}?${new URLSearchParams(t).toString()}`:e}function o(e){return n("/admin/",e)}function a(e){return n("/admin/oauth/logout/",e)}function s(e){return n("/today/",e)}r.d(t,{HF:()=>o,Kn:()=>a,tG:()=>s})}}]);
//# sourceMappingURL=https://sm.pinimg.com/webapp/12506-2c05c2654d5d5db1.mjs.map