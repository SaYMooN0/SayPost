const manifest = (() => {
function __memo(fn) {
	let value;
	return () => value ??= (value = fn());
}

return {
	appDir: "_app",
	appPath: "_app",
	assets: new Set(["app.css","email-sent.svg","favicon.png"]),
	mimeTypes: {".css":"text/css",".svg":"image/svg+xml",".png":"image/png"},
	_: {
		client: {start:"_app/immutable/entry/start.Xmfud45D.js",app:"_app/immutable/entry/app.7w-tSbDE.js",imports:["_app/immutable/entry/start.Xmfud45D.js","_app/immutable/chunks/CnPPPaJN.js","_app/immutable/chunks/DVUoXnul.js","_app/immutable/chunks/TlERTnYi.js","_app/immutable/chunks/DIeogL5L.js","_app/immutable/chunks/CYgJF_JY.js","_app/immutable/entry/app.7w-tSbDE.js","_app/immutable/chunks/TlERTnYi.js","_app/immutable/chunks/DIeogL5L.js","_app/immutable/chunks/CeeueTis.js","_app/immutable/chunks/CWj6FrbW.js","_app/immutable/chunks/GGSi--Df.js","_app/immutable/chunks/XGGrSNVo.js","_app/immutable/chunks/BC8zxhgT.js","_app/immutable/chunks/DVUoXnul.js"],stylesheets:[],fonts:[],uses_env_dynamic_public:false},
		nodes: [
			__memo(() => import('./chunks/0-LwMRa1hJ.js')),
			__memo(() => import('./chunks/1-Cv4zPNZT.js')),
			__memo(() => import('./chunks/2-CPugkvBI.js')),
			__memo(() => import('./chunks/3-BV_s1hY4.js')),
			__memo(() => import('./chunks/4-CLjtud9L.js')),
			__memo(() => import('./chunks/5-DJad3nQd.js')),
			__memo(() => import('./chunks/6-XO3VA8A3.js')),
			__memo(() => import('./chunks/7-B0t_iXbv.js')),
			__memo(() => import('./chunks/8-CQEpjEPr.js')),
			__memo(() => import('./chunks/9-DpBDsWps.js')),
			__memo(() => import('./chunks/10-DCofXQbi.js')),
			__memo(() => import('./chunks/11-D4ykPdbf.js')),
			__memo(() => import('./chunks/12-Dep4gLAN.js')),
			__memo(() => import('./chunks/13-BilwJMbh.js'))
		],
		routes: [
			{
				id: "/",
				pattern: /^\/$/,
				params: [],
				page: { layouts: [0,], errors: [1,], leaf: 3 },
				endpoint: null
			},
			{
				id: "/confirm-registration/[userId]/[confirmationCode]",
				pattern: /^\/confirm-registration\/([^/]+?)\/([^/]+?)\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false},{"name":"confirmationCode","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,], errors: [1,], leaf: 4 },
				endpoint: null
			},
			{
				id: "/new-post/[[selectedPostId]]",
				pattern: /^\/new-post(?:\/([^/]+))?\/?$/,
				params: [{"name":"selectedPostId","optional":true,"rest":false,"chained":true}],
				page: { layouts: [0,], errors: [1,], leaf: 5 },
				endpoint: null
			},
			{
				id: "/posts",
				pattern: /^\/posts\/?$/,
				params: [],
				page: { layouts: [0,], errors: [1,], leaf: 6 },
				endpoint: null
			},
			{
				id: "/posts/[postId]",
				pattern: /^\/posts\/([^/]+?)\/?$/,
				params: [{"name":"postId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,], errors: [1,], leaf: 7 },
				endpoint: null
			},
			{
				id: "/users/[userId]",
				pattern: /^\/users\/([^/]+?)\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,], errors: [1,], leaf: 8 },
				endpoint: null
			},
			{
				id: "/users/[userId]/(sub-pages)/comments-left",
				pattern: /^\/users\/([^/]+?)\/comments-left\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,2,], errors: [1,,], leaf: 9 },
				endpoint: null
			},
			{
				id: "/users/[userId]/(sub-pages)/followers",
				pattern: /^\/users\/([^/]+?)\/followers\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,2,], errors: [1,,], leaf: 10 },
				endpoint: null
			},
			{
				id: "/users/[userId]/(sub-pages)/followings",
				pattern: /^\/users\/([^/]+?)\/followings\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,2,], errors: [1,,], leaf: 11 },
				endpoint: null
			},
			{
				id: "/users/[userId]/(sub-pages)/liked-posts",
				pattern: /^\/users\/([^/]+?)\/liked-posts\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,2,], errors: [1,,], leaf: 12 },
				endpoint: null
			},
			{
				id: "/users/[userId]/(sub-pages)/published-posts",
				pattern: /^\/users\/([^/]+?)\/published-posts\/?$/,
				params: [{"name":"userId","optional":false,"rest":false,"chained":false}],
				page: { layouts: [0,2,], errors: [1,,], leaf: 13 },
				endpoint: null
			}
		],
		prerendered_routes: new Set([]),
		matchers: async () => {
			
			return {  };
		},
		server_assets: {}
	}
}
})();

const prerendered = new Set([]);

const base = "";

export { base, manifest, prerendered };
//# sourceMappingURL=manifest.js.map
