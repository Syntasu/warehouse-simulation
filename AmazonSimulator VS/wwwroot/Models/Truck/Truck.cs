class Truck extends THREE.Group
{

    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;


        // loading the texture
        var mtlLoader = new THREE.MTLLoader();
        mtlLoader.setPath('Models/Truck/');
        var url = "CUPIC_TRUCK.mtl";
        mtlLoader.load(url, function(materials) {

            materials.preload();
            // loading the 3D model
            var objLoader = new THREE.OBJLoader();
            objLoader.setMaterials(materials);
            objLoader.setPath('Models/Truck/');
            objLoader.load('CUPIC_TRUCK.obj', function(object) {
                var group = new THREE.Group();
                object.scale.set(10, 10, 10);
                object.rotation.y = Math.PI / 2;
                object.position.y = 1;
                object.position.z = -2;

                group.add(object);


            selfRef.add(group);
        });

    });
}

}
