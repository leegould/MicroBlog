define(["jquery", "react", "reacttransformer"], function($) {

    $(function() {
        var mockdata = [
            { id: 1, title: "Test Title 1", content: "Test Content 1" },
            { id: 2, title: "Test Title 2", content: "Test Content 2" }
        ];

        var BlogSummary = React.createClass({
            render: function() {
                return( <div className = "row">
                            <div className = "blogheader"><a href ={ this.props.blog.id }>{ this.props.blog.title }</a></div> 
                            <div><a href = { this.props.blog.id } >{ this.props.blog.content }</a></div> 
                       </div>);
            }
        });

        var BlogList = React.createClass({
            render: function() {
                var rows = [];

                if (this.props.blogs == undefined) {
                    return false;
                }

                this.props.blogs.forEach(function(blog) {
                    rows.push(<BlogSummary blog = { blog }/>);
                }.bind(this));
                
                return (<div> 
                            <h1>My Blogs</h1>
                            {rows}
                        </div>);
            }
        });

        React.render(<BlogList blogs = { mockdata } /> , document.getElementById('content'));
    });
});