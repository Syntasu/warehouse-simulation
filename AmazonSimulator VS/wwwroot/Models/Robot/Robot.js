class Robot extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;
        // Dimensions of the robot
        var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);

        // loading in the texture for the robot.
        var robotmaterials = [
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_side.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_top.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_front.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("Models/Robot/robot_front.png"), side: THREE.DoubleSide }), //BACK
        ];

        var robot = new THREE.Mesh(geometry, robotmaterials);
        // Start Position of the robot.
        robot.position.y = 0.15;

        robot.name = "robot";
        robot.userData = { status: "idle" };

        var group = new THREE.Group();
        selfRef.add(robot);
    }
}
