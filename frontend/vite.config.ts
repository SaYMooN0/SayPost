import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'^/api/auth': {
				target: 'http://localhost:5177/',
				secure: false,
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/api\/auth/, '')

			},
			'^/api/main': {
				target: 'http://localhost:5124/',
				secure: false,
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/api\/main/, '')

			},
			'^/api/notifications': {
				target: 'http://localhost:5026/',
				secure: false,
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/api\/notifications/, '')
			},
			'^/api/followings': {
				target: 'http://localhost:5250/',
				secure: false,
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/api\/followings/, '')
			},

		},
		port: 5173
		// ,https: {
		//     key: fs.readFileSync(keyFilePath),
		//     cert: fs.readFileSync(certFilePath),
		// }
	}
});
