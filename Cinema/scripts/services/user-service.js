const userService = (() => {
          function isAuth() {
              return sessionStorage.getItem('authtoken') !== null;
          }

          function saveSession(res) {
              sessionStorage.setItem('username', res.username);
              sessionStorage.setItem('authtoken', res._kmd.authtoken);
              sessionStorage.setItem('id',res._id);
          }

          function register(username, password) {
              return kinvey.post('user', '', 'basic', {
                  username,
                  password
              })
          }

          function login(username, password) {
              return kinvey.post('user', 'login', 'basic', {
                  username,
                  password
              });
  }

  function logout() {
    return kinvey.post('user', '_logout', 'kinvey');
  }
    function createMovie(data) {
        return kinvey.post('appdata', 'movies', 'kinvey',data);
    }
    function getAllMovies() {
        return kinvey.get('appdata', 'movies?query={}&sort={"tickets": -1}', 'kinvey');
    }
    function myMovies() {

        return kinvey.get('appdata',`movies?query={"_acl.creator":"${sessionStorage.getItem('id')}"}`,'kinvey');

    }
    function removeMovie(id) {

        return kinvey.remove('appdata',`movies/${id}`,'kinvey');

    }
    function getOneMovie(id) {

        return kinvey.get('appdata',`movies/${id}`,'kinvey');

    }
    function buyTicket(id,data) {

        return kinvey.update('appdata',`movies/${id}`,'kinvey',data);

    }
    function updateMovie(id,data) {

        return kinvey.update('appdata',`movies/${id}`,'kinvey',data);

    }
  return {
    register,
    login,
    logout,
    saveSession,
    isAuth,
      createMovie,
      getAllMovies,
      myMovies,
      removeMovie,
      getOneMovie,
      buyTicket,
      updateMovie
  }
})()