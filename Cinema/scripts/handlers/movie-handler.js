handlers.createMovie = function (ctx) {
    ctx.isAuth = userService.isAuth();
    ctx.username = sessionStorage.getItem('username');

    ctx.loadPartials({
        header: 'templates/common/header.hbs',
        footer: 'templates/common/footer.hbs'
    }).then(function () {
        this.partial('templates/common/createMovie.hbs');
    }).catch(function (err) {
        console.log(err);
    });
}
handlers.createMovieCreator = function (ctx) {
    let genres=ctx.params.genres.split(" ");
    console.log(genres);
let movie={title:ctx.params.title,imageUrl:ctx.params.imageUrl,description:ctx.params.description,genres:genres,tickets:ctx.params.tickets};
    ctx.isAuth = userService.isAuth();

    ctx.username = sessionStorage.getItem('username');
    if (movie.title.length < 6) {
        notifications.showError("movie title must be atleast 6 characters long")
    }
    else if(!movie.imageUrl.startsWith('http://')&&!movie.imageUrl.startsWith('https://')) {
        notifications.showError("imageUrl must start with http:// or https://")
    }
    else if(movie.description.length<10) {
        notifications.showError("movie description must be atleast 10 characters long")
    }

    else if(!Number(movie.tickets)){
        notifications.showError("tickets must be a number")
    }

    else {

        userService.createMovie(movie).then(function () {

            notifications.showSuccess('movie created successfully');
            ctx.redirect('#/home');
        }).catch(function (err) {
            notifications.showError(err.responseJSON.description);
        });


    }

    $("#createTitle").val("");
    $("#createImage").val("");
    $("#createDescription").val("");
    $("#createGenres").val("");
    $("#createTickets").val("");

}
handlers.getAllMovies = function (ctx) {

    ctx.isAuth = userService.isAuth();
    ctx.username = sessionStorage.getItem('username');
    userService.getAllMovies().then(function (movies) {
        ctx.movies = movies;
        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs',
            movie: 'templates/movie.hbs'
        }).then(function () {
            this.partial('templates/cinema.hbs');
        }).catch(function (err) {
            console.log(err);
        });

    })
}
handlers.getMyMovies=function (ctx) {
    ctx.isAuth = userService.isAuth();
    ctx.username = sessionStorage.getItem('username');
    let id=sessionStorage.getItem('id');


    userService.myMovies().then(function (movies) {

        let movies2=movies;
       movies2.forEach((movie) => movie.isCreator=movie._acl.creator===id);
        ctx.movies=movies2;

        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs',
            movie: 'templates/movie.hbs'
        }).then(function () {
            this.partial('templates/myMovies.hbs');
        }).catch(function (err) {
            console.log(err);
        });

    })
}
handlers.deletedMovie = function (ctx) {
    userService.removeMovie(ctx.params.id).then(function () {
        notifications.showSuccess('movie removed successfully!');
        ctx.redirect('#/myMovies');
    }).catch(function (err) {
        notifications.showError(err.responseJSON.description);
    });



}
handlers.buyTicket = function (ctx) {
    console.log(ctx.app.last_route);
    console.log('hiii');
    ctx.isAuth = userService.isAuth();
    ctx.username = sessionStorage.getItem('username');
    if(ctx.isAuth) {
        userService.getOneMovie(ctx.params.id).then(function (res) {
            if (res.tickets <= 0) {
                notifications.showError(`no more tickets available for ${res.title}`)
            }
            else {
                let tickets = res.tickets;
                tickets = Number(tickets) - 1;
                res.tickets = tickets;
                userService.buyTicket(ctx.params.id, res).then(function () {

                    notifications.showSuccess(`Successfully bought ticket for ${res.title}`);
                    ctx.redirect('#/allMovies');
                }).catch(function (err) {
                    notifications.showError(err.responseJSON.description);
                });
            }

        })
        console.log(ctx.app.last_route);
    }



}

handlers.getUpdateMovie= function (ctx) {
    console.log(ctx);

    ctx.isAuth = userService.isAuth();

    ctx.username = sessionStorage.getItem('username');
console.log(ctx);
    userService.getOneMovie(ctx.params.id).then(function (movie) {
console.log(movie);
         Object.assign(ctx,movie);
        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs'
        }).then(function () {
            this.partial('templates/updateMovie.hbs');
        }).catch(function (err) {
            console.log(err);
        });
})
}
handlers.deleteMovie = function (ctx) {
    ctx.isAuth = userService.isAuth();
    let newDescription = ctx.params.description;
    ctx.username = sessionStorage.getItem('username');
    console.log(ctx);
    userService.getOneMovie(ctx.params.id).then(function (movie) {
        console.log(movie);
        console.log(movie._id);
      Object.assign(ctx,movie);
        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs'
        }).then(function () {
            this.partial('templates/deleteMovie.hbs');
        }).catch(function (err) {
            console.log(err);
        });
    })
}
handlers.PostUpdateMovie = function (ctx) {
    ctx.isAuth = userService.isAuth();
    let newDescription = ctx.params.description;
    ctx.username = sessionStorage.getItem('username');
    console.log(ctx);
    if (ctx.params.title.length < 6) {
        notifications.showError("movie title must be atleast 6 characters long")
    }
    else if (!ctx.params.imageUrl.startsWith('http://') && ! ctx.params.imageUrl.startsWith('https://')) {
        notifications.showError("imageUrl must start with http:// or https://")
    }
    else if (ctx.params.description.length < 10) {
        notifications.showError("movie description must be atleast 10 characters long")
    }

    else if (!Number(ctx.params.tickets)) {
        notifications.showError("tickets must be a number")
    }
    else {
        userService.getOneMovie(ctx.params.id).then(function (movie) {
            let editedMovie = {
                title: ctx.params.title,
                imageUrl: ctx.params.imageUrl,
                description: ctx.params.description,
                genres: ctx.params.genres,
                tickets: ctx.params.tickets
            }
            userService.updateMovie(ctx.params.id, editedMovie).then(function () {
                notifications.showSuccess("movie updated successfully")
                ctx.redirect('#/myMovies');

            })
        })
    }

}


handlers.getDetails=function (ctx) {
    ctx.isAuth = userService.isAuth();
    console.log(ctx.params.id);
    ctx.username = sessionStorage.getItem('username');
    userService.getOneMovie(ctx.params.id).then(function (movie) {

        Object.assign(ctx, movie);

        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs'
        }).then(function () {
            this.partial('templates/movieDetails.hbs');
        }).catch(function (err) {
            console.log(err);
        });

    })
}
handlers.filteredMovies=function (ctx) {
    let filter=ctx.params.search;
    ctx.isAuth = userService.isAuth();
    ctx.username = sessionStorage.getItem('username');
    userService.getAllMovies().then(function (movies) {
        console.log(movies);
        movies=movies.filter(movie=>movie.genres.includes(filter));
        ctx.movies = movies;
        ctx.loadPartials({
            header: 'templates/common/header.hbs',
            footer: 'templates/common/footer.hbs',
            movie: 'templates/movie.hbs'
        }).then(function () {
            this.partial('templates/cinema.hbs');
        }).catch(function (err) {
            console.log(err);
        });

    })
}
