const handlers = {}

$(() => {
  const app = Sammy('#container', function () {
    this.use('Handlebars', 'hbs');
    // home page routes
    this.get('/index.html', handlers.getHome);
    this.get('/', handlers.getHome);
    this.get('#/home', handlers.getHome);

    // user routes
    this.get('#/register', handlers.getRegister);
    this.get('#/login', handlers.getLogin);

    this.post('#/register', handlers.registerUser);
    this.post('#/login', handlers.loginUser);
    this.get('#/logout', handlers.logoutUser);
    this.get('#/allMovies',handlers.getAllMovies);
    this.get('#/create',handlers.createMovie);
    this.post('#/create',handlers.createMovieCreator);
    this.get('#/myMovies',handlers.getMyMovies);
this.get('#/delete/:id',handlers.deleteMovie);
      this.get('#/buyTicket/:id',handlers.buyTicket);

      this.get('#/edit/:id',handlers.getUpdateMovie);

      this.post('#/edit/:id',handlers.PostUpdateMovie);
      this.post('#/delete/:id',handlers.deletedMovie);

      this.get('#/details/:id',handlers.getDetails);
      this.get('#/filteredMovies',handlers.filteredMovies);

    // ADD YOUR ROUTES HERE
  });
  app.run('#/home');
});