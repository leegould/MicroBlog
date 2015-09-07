var BlogEdit = React.createClass({
    loadData: function() {
        var urlParams;
        (window.onpopstate = function () {
            var match,
            pl = /\+/g,  // Regex for replacing addition symbol with a space
            search = /([^&=]+)=?([^&]*)/g,
            decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
            query = window.location.search.substring(1);

            urlParams = {};
            while (match = search.exec(query))
                urlParams[decode(match[1])] = decode(match[2]);
        })();

        $.ajax({
            url: "api/" + urlParams["id"],
            dataType: 'json',
            cache: false,
            success: function(data) {
                this.setState({title: data.title, content: data.content, id: data.id});
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    getInitialState: function() {
        return { data: {} };
    },
    handleTitleChange: function(event) {
        this.setState({title: event.target.value});
    },
    componentDidMount: function() {
        this.loadData();
    },
    render: function() {
        return (
            
            <form id="blogpostform">
                <h1>Enter the content for your new blogpost</h1>
                <div>
                    <label for="title"></label>
                    <input type="text" id="title" value={this.state.title} onChange={this.handleTitleChange} />
                </div>
                <div>
                    <div className="editor">
                        { this.state.content }
                    </div>
                </div>
                <div>
                    <input type="submit" value="Update" />
                    <a className="link" href="index.html">Home</a>
                </div>
            </form>
        );
    }
});